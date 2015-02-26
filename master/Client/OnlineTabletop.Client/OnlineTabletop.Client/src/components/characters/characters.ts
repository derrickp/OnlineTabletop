/// <amd-dependency path="text!./characters.html" />
import ko = require("knockout");
export var template: string = require("text!./characters.html");

export class viewModel {

    public message = ko.observable("Hello from the characters component!");

    constructor (params: any) {

    }

    public dispose() {
        // This runs when the component is torn down. Put here any logic necessary to clean up,
        // for example cancelling setTimeouts or disposing Knockout subscriptions/computeds.        
    }
}
