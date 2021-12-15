import { Applicant } from 'models/Applicant';
import { Notification } from 'models/Notification';
import { inject, NewInstance } from 'aurelia-framework';
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.css';
import { BootstrapFormRenderer } from '../static/bootstrap-form-renderer';
import { ValidationRules, ValidationController } from 'aurelia-validation';
import { ApplicantService } from 'services/applicant-service';
import { computedFrom } from 'aurelia-framework';

import { Prompt } from 'modal';
import { DialogService } from 'aurelia-dialog';
import { I18N } from 'aurelia-i18n';
@inject(Applicant, ApplicantService, NewInstance.of(ValidationController), DialogService, I18N)
export class App {
  message: any;
  model: any;
  controller = null;
  countries: any = null;
  defaultHiredStatus = null;
  dialogService = null;
  service = null;
  notification: Notification;
  applicants: Applicant[];
  i18n = null;
  btnMode = "submit";

  constructor(applicant, applicantService, controller, dialogService, i18n: I18N) {
    this.model = applicant;
    this.controller = controller;
    this.controller.addRenderer(new BootstrapFormRenderer());
    this.configureValidationRules();
    this.service = applicantService;
    this.defaultHiredStatus = this.model.isHired;
    this.service.getCountries().then(data => {
      this.countries = data;
    });
    this.dialogService = dialogService
    this.i18n = i18n;
    this.i18n.setLocale('en');
    //dialogController.settings.centerHorizontalOnly = true;
    this.getApplicants();
  }

  @computedFrom('model.name', 'model.familyName', 'model.email', 'model.age', 'model.address', 'model.countryOfOrigin', 'model.defaultHiredStatus')
  get hasChanged() {

    return (this.model.name != ''
      && this.model.familyName != ''
      && this.model.email != ''
      && this.model.age != ''
      && this.model.address != ''
      && this.model.countryOfOrigin != '--Select--'
    );
  }

  configureValidationRules() {
    ValidationRules.customRule(
      'validCountryName',
      (value, obj) => value === null || value != "--Select--",
      `Please select valid country.`
    );

    ValidationRules.customRule(
      'addressMinLength',
      (value, obj) => value != null && value.length >= 10,
      ``
    );
    ValidationRules
      .ensure('name').required().minLength(5)
      .ensure('familyName').required().minLength(5)
      .ensure('email').required().email()
      .ensure('age').required()
      .ensure('age').range(20, 60)
      .ensure('address').required().satisfiesRule('addressMinLength')
      .ensure('isHired').required()
      .ensure('countryOfOrigin').required()
      .satisfiesRule('validCountryName')
      .on(this.model);
  }

  openModal(notificationModel) {
    this.dialogService.open({ viewModel: Prompt, model: notificationModel }).whenClosed(response => {

      if (!response.wasCancelled) {
        this.clearForm();
      }
      else {
        console.log('cancelled');
      }

    });
  }
  reset() {
    this.notification = { message: 'Are you sure to reset this form?', IsConfirmBox: true };
    this.openModal(this.notification);

  }

  clearForm() {
    this.controller.removeRenderer(new BootstrapFormRenderer());
    this.model.name = '';
    this.model.familyName = '';
    this.model.email = '';
    this.model.age = null;
    this.defaultHiredStatus = false;
    this.model.countryOfOrigin = ' ';
    this.model.address = '';

    var formGroup = document.getElementsByClassName("form-group");
    for (let i = 0; i < formGroup.length; i++) {
      formGroup[i].classList.remove('has-success');
      formGroup[i].classList.remove('has-danger');
    }
    setTimeout(function () {
      for (let i = 0; i < formGroup.length; i++) {
        formGroup[i].classList.remove('has-success');
        formGroup[i].classList.remove('has-danger');
      }
    }, 100);
    this.controller.addRenderer(new BootstrapFormRenderer());
  }

  setLocale(locale) {
    this.i18n.setLocale(locale);
  }

  edit(applicant: Applicant) {
    this.model = applicant;
    this.btnMode = "update";
  }
  //CRUD OPERATION METHODS

  add(model) {
    model.isHired = this.defaultHiredStatus;
    model.age = parseInt(model.age)
    this.service.postData(model)
      .then(response => {
        if (response.status == 200) {
          this.notification = { message: 'Applicant added successfully.', IsConfirmBox: false };
          this.getApplicants();
        }
        else {
          this.notification = { message: 'An error accurred while adding applicant.', IsConfirmBox: false };
        }
        this.openModal(this.notification);
      })
      .catch(error => {
        console.log('Error in adding applicant.');
      });
  }

  update(model) {
    model.isHired = this.defaultHiredStatus;
    model.age = parseInt(model.age)
    this.service.updateData(model)
      .then(response => {
        if (response.status == 200) {
          this.notification = { message: 'Applicant updated successfully.', IsConfirmBox: false };
          this.getApplicants();
          this.btnMode = "submit";
        }
        else {
          this.notification = { message: 'An error accurred while updating applicant.', IsConfirmBox: false };
        }
        this.openModal(this.notification);
      })
      .catch(error => {
        console.log('Error in updating applicant.');
      });
  }
  getApplicants() {
    this.service.getData()
      .then(data => {
        if (data.length > 0)
          this.applicants = data;
        else this.applicants = [];
      })
      .catch(error => {
        console.log('Error in retrieving applicant.');
      })
  }

  delete(id) {
    this.service.deleteData(id)
      .then(response => {
        if (response.status == 200) {
          this.notification = { message: 'Applicant deleted successfully.', IsConfirmBox: false };
          this.getApplicants();
        }
        else {
          this.notification = { message: 'An error accurred while deleting applicant.', IsConfirmBox: false };
        }
        this.openModal(this.notification);
      })
      .catch(error => {
        console.log('Error in deleting applicant.');
      });
  }
}
