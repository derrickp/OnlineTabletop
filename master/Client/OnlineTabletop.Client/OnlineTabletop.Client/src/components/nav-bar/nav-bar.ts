/// <amd-dependency path="text!./nav-bar.html" />
import ko = require("knockout");
import um = require("../../local_modules/user-manager/user-manager");
export var template: string = require("text!./nav-bar.html");

export class viewModel {
    public route: any;
    public isNotLoggedIn: KnockoutObservable<boolean> = ko.observable(true);
    public isLoggedIn: KnockoutObservable<boolean> = ko.observable(false);

    constructor(params: any) {
        // This viewmodel doesn't do anything except pass through the 'route' parameter to the view.
        // You could remove this viewmodel entirely, and define 'nav-bar' as a template-only component.
        // But in most apps, you'll want some viewmodel logic to determine what navigation options appear.

        um.User.subscribe((newValue: um.UserInfo) => {
            this.handleUserChanged(newValue);
            window.location.hash = '';
        });
        if (um && um.User) {
            this.handleUserChanged(um.User());
        }
        else {
            this.handleUserChanged(null);
        }

        this.route = params.route;
    }

    handleUserChanged(userInfo: um.UserInfo) {
        if (userInfo && userInfo.isAuthenticated) {
            this.isNotLoggedIn(!userInfo.isAuthenticated());
            this.isLoggedIn(userInfo.isAuthenticated());
        }
        else {
            this.isNotLoggedIn(true);
            this.isLoggedIn(false);
        }
    }
}
