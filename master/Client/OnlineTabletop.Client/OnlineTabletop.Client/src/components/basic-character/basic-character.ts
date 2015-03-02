/// <amd-dependency path="text!./basic-character.html" />
import ko = require("knockout");
export var template: string = require("text!./basic-character.html");

export class viewModel {
    
    constructor (params: any) {
        debugger;
    }

    public dispose() {
        // This runs when the component is torn down. Put here any logic necessary to clean up,
        // for example cancelling setTimeouts or disposing Knockout subscriptions/computeds.        
    }
}
