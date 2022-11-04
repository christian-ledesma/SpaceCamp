import { makeAutoObservable, reaction } from "mobx";
import { ServerError } from "../models/ServerError";

export default class CommonStore {
    error: ServerError | null = null;
    token: string | null = window.localStorage.getItem("Token");
    appLoaded = false;

    constructor() {
        makeAutoObservable(this);

        reaction(() => this.token, token => {
            if (token) {
                window.localStorage.setItem("Token", token);
            } else {
                window.localStorage.removeItem("Token");
            }
        })
    }

    setServerError = (error: ServerError) => {
        this.error = error;
    }

    setToken = (token: string | null) => {
        this.token = token;
    }

    setAppLoaded = () => {
        this.appLoaded = true;
    }
}
