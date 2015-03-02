import characterDto = require("../../models/character-dtos");
import ko = require("knockout");

export class character {
    name: KnockoutObservable<string> = ko.observable("");

    constructor(inputDto: characterDto.basicCharacterDTO) {
        this.name(inputDto.name);
    }
}