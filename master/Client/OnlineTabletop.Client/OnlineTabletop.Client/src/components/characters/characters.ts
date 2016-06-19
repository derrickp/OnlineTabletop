/// <amd-dependency path="text!./characters.html" />

import ko = require("knockout");
import um = require("../../local_modules/user-manager/user-manager");
import characterDto = require("../../models/character-dtos");
import characterInfo = require("../../local_modules/character-info/character-info");
export var template: string = require("text!./characters.html");

export class viewModel {

    characters: KnockoutObservableArray<characterInfo.character> = ko.observableArray([]);

    constructor(params: any) {

        var self = this;
        if (um && um.User && um.User().isAuthenticated && !um.User().isAuthenticated()) {
            window.location.hash = 'login';
        }

        um.getCharacters().done((results: Array<characterDto.basicCharacterDTO>) => {
            if (results) {
                results.forEach((value: characterDto.basicCharacterDTO) => {
                    var character = new characterInfo.character(value);
                    this.characters.push(character);
                });
            }
        }).fail((error: Error) => {

        });
    }

    selectCharacter(args) {
        debugger;
    }

    public dispose() {
        // This runs when the component is torn down. Put here any logic necessary to clean up,
        // for example cancelling setTimeouts or disposing Knockout subscriptions/computeds.        
    }
}
