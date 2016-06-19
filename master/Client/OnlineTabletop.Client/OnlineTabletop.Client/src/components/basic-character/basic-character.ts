/// <amd-dependency path="text!./basic-character.html" />
import ko = require("knockout");
import characterInfo = require("../../local_modules/character-info/character-info");
export var template: string = require("text!./basic-character.html");

export class viewModel {

    character: KnockoutObservable<characterInfo.character>;

    constructor(params: any) {
        if (params.character) {
            this.character = params.character;
        }
        debugger;
    }

    public dispose() {
        // This runs when the component is torn down. Put here any logic necessary to clean up,
        // for example cancelling setTimeouts or disposing Knockout subscriptions/computeds.        
    }
}
