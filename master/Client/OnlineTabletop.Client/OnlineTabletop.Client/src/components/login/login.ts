/// <amd-dependency path="text!./login.html" />

import ko = require("knockout");
import $ = require("jquery");
import um = require("../../local_modules/user-manager/user-manager");
export var template: string = require("text!./login.html");

export class viewModel {
    public password: KnockoutObservable<string> = ko.observable("");
    public userName: KnockoutObservable<string> = ko.observable("");
    public validationError: KnockoutObservable<string> = ko.observable(null);

    constructor (params: any) {

    }

    public loginUser(stuff) {
        if (this.userName() === "") {
            this.validationError("User name cannot be empty.");
            return;
        }
        if (this.password() === "") {
            this.validationError("Password cannot be empty.");
            return;
        }
        this.validationError(null);
        $.post("http://localhost:63096/token", {
            grant_type: "password",
            username: this.userName(),
            password: this.password()
        }, (data: um.TokenData, status: string, jqXHR: JQueryXHR) => {
            um.setData(data);
        }).fail((error) => {
            alert("Invalid username or password");
        });
    }

    public dispose() {
        this.userName(null);
        this.password(null);
        // This runs when the component is torn down. Put here any logic necessary to clean up,
        // for example cancelling setTimeouts or disposing Knockout subscriptions/computeds.        
    }
}
