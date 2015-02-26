/// <reference path="../local_modules/user-manager/user-manager.ts" />

import $ = require("jquery");
import ko = require("knockout");
import bootstrap = require("bootstrap");
import um = require('../local_modules/user-manager/user-manager');
import router = require("./router");

// Components can be packaged as AMD modules, such as the following:
ko.components.register('nav-bar', { require: 'components/nav-bar/nav-bar' });
ko.components.register('home-page', { require: 'components/home-page/home' });

// ... or for template-only components, you can just point to a .html file directly:
ko.components.register('about-page', {
  template: { require: 'text!components/about-page/about.html' }
});

ko.components.register('login', { require: 'components/login/login' });

ko.components.register('user-info', { require: 'components/user-info/user-info' });


ko.components.register('characters', { require: 'components/characters/characters' });



// [Scaffolded component registrations will be inserted here. To retain this feature, don't remove this comment.]

um.initialize();

// Start the application
ko.applyBindings({ route: router.currentRoute });
