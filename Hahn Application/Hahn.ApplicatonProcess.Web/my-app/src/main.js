import {Aurelia} from 'aurelia-framework';
import * as environment from '../config/environment.json';
import {PLATFORM} from 'aurelia-pal';
import {I18N} from 'aurelia-i18n';
import XHR from 'i18next-xhr-backend';
import {ValidationMessageProvider} from 'aurelia-validation';
export function configure(aurelia) {
  aurelia.use
    .standardConfiguration()
    .plugin(PLATFORM.moduleName('aurelia-validation'))
    .plugin(PLATFORM.moduleName('aurelia-dialog'))
    .plugin('aurelia-dialog')
    //.plugin('aurelia-dialog')
    
    .plugin(PLATFORM.moduleName('aurelia-i18n'), (instance) => {
      // register backend plugin
      instance.i18next.use(XHR);

      // adapt options to your needs (see http://i18next.com/docs/options/)
      instance.setup({
         backend: {    allowMultiLoading: true,                              
            loadPath: 'locales/{{lng}}/{{ns}}.json',
         },
				
         // : 'de',
         attributes : ['t','i18n'],
         fallbackLng : 'en',
         debug : false
      });
   })
   
  .feature(PLATFORM.moduleName('resources/index'));
    ValidationMessageProvider.prototype.getMessage = function(key) {
      console.log("key :"+key);
      const i18n = aurelia.container.get(I18N);
      const translation = i18n.tr('errorMessages.'+key);
    console.log("parse "+translation);
      return this.parser.parse(translation);
    };

    ValidationMessageProvider.prototype.getDisplayName = function(propertyName, displayName) {
    //  console.log('pro :'+propertyName+' dp:'+displayName);
      if (displayName !== null && displayName !== undefined) {
        return displayName;
      }
      const i18n = aurelia.container.get(I18N);
     // console.log("i18n"+i18n.tr(propertyName));
      return  i18n.tr(propertyName);
    };
    
  aurelia.use.developmentLogging(environment.debug ? 'debug' : 'warn');

  if (environment.testing) {
    aurelia.use.plugin(PLATFORM.moduleName('aurelia-testing'));
  }
  
  aurelia.start().then(() => aurelia.setRoot(PLATFORM.moduleName('app')));
}
