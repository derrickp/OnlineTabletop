import classDto = require("rpgClass-dtos");

export interface basicCharacterDTO {
    id: string;

    name: string;

    playerAccountName: string;

    race: string;
    classes: Array<classDto.rpgClassDTO>;
} 