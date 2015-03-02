/// <reference path="../../models/player-dtos.ts" />

import ko = require("knockout");
import $ = require("jquery");
import playerDto = require("../../models/player-dtos");

export class UserInfo {
    _dto: playerDto.basicPlayerDTO;
    public name: KnockoutObservable<string> = ko.observable("");
    public email: KnockoutObservable<string> = ko.observable("");
    public joinDateFormat: KnockoutObservable<string> = ko.observable("");
    public displayName: KnockoutObservable<string> = ko.observable("");
    public isAuthenticated: KnockoutObservable<boolean> = ko.observable(false);
    _id: string;

    constructor(playerDTO?: playerDto.basicPlayerDTO) {
        if (playerDTO) {
            this._dto = playerDTO;
            this._id = playerDTO.id;
            this.name(playerDTO.name);
            this.email(playerDTO.email);
            if (playerDTO.joinDate && typeof playerDTO.joinDate !== "date") {
                playerDTO.joinDate = new Date(<any>playerDTO.joinDate);
            }
            this.joinDateFormat(playerDTO.joinDate.toLocaleString(['en-GB', 'en-US'], { hour12: false, weekday: "short", year: "numeric", month: "long", day: "numeric" }));
            if (playerDTO.displayName) {
                this.displayName(playerDTO.displayName);
            }
        }
    }
}

export var User: KnockoutObservable<UserInfo> = ko.observable(new UserInfo());

export function getUserToken(): string {
    if (localStorage && localStorage.getItem("tokenData")) {
        var tokenData = JSON.parse(localStorage.getItem("tokenData"));
    }
    return "";
}

export function setData(data: TokenData) {
    // Check the expiry of the data.
    if (data.expires_in == 0) {
        // Refresh the token.
        return;
    }
    else {
        if (localStorage) {
            localStorage.setItem("tokenData", JSON.stringify(data));
        }
        _getUserInfo(data);
    }
}

export function setUserInfo(playerDTO: playerDto.basicPlayerDTO) {
    var userInfo = new UserInfo(playerDTO);
    userInfo.isAuthenticated(true);
    User(userInfo);
}

export function getCharacters(): JQueryXHR {
    var data: TokenData = JSON.parse(localStorage.getItem("tokenData"));
    
    var deferred = $.ajax("http://localhost:63096/characters", {
        beforeSend: (xhr: JQueryXHR) => {
            xhr.setRequestHeader("Authorization", "Bearer " + data.access_token);
        },
        accepts: "json"
    });

    return deferred;
}

function _getUserInfo(data: TokenData) {
    $.ajax("http://localhost:63096/player/info", {
        beforeSend: (xhr: JQueryXHR) => {
            xhr.setRequestHeader("Authorization", "Bearer " + data.access_token);
        },
        accepts: "json",
        success: (result: playerDto.basicPlayerDTO) => {
            if (result) {
                setUserInfo(result);
            }
        },
        type: 'GET'
    }).fail((error) => {
        debugger;
    });
}

export function initialize() {
    // In here we have to check whether or not the token has expired. If it has, we'd need to refresh the token.
    if (localStorage && localStorage.getItem("tokenData")) {
        var tokenData = JSON.parse(localStorage.getItem("tokenData"));
        setData(tokenData);
    }
}

export interface TokenData {
    access_token: string;
    expires_in: number;
    token_type: string;
}