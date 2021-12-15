import {inject} from 'aurelia-framework';
import {DialogController} from 'aurelia-dialog';

@inject(DialogController)

export class Prompt {
   dialogcontroller=null
   notification=null;
   constructor(controller) {
      this.dialogcontroller = controller;
      
      controller.settings.centerHorizontalOnly = true;
   }
   activate(notification) {
      this.notification = notification;
   }
   cancel()
   {
      
   }
   ok(msg)
   {
      console.log(msg);
   }
}