/// <amd-dependency path="text!./user-info.html" />
import ko = require("knockout");
import um = require("../../local_modules/user-manager/user-manager");
export var template: string = require("text!./user-info.html");

export class viewModel {

    public user = um.User;

    constructor (params: any) {
        debugger;
        if (um && um.User && um.User().isAuthenticated && !um.User().isAuthenticated()) {
            window.location.hash = 'login';
        }
    }

    public dispose() {
        // This runs when the component is torn down. Put here any logic necessary to clean up,
        // for example cancelling setTimeouts or disposing Knockout subscriptions/computeds.        
    }
}
