import { makeAutoObservable, runInAction } from "mobx";
import { history } from "../..";
import { User, UserFormValues } from "../models/User";
import agent from "../services/agent";
import { store } from "./store";

export default class UserStore {
    user: User | null = null;

    constructor() {
        makeAutoObservable(this)
    }

    get isLoggedIn() {
        return !!this.user;
    }

    login = async (credentials: UserFormValues) => {
        try {
            const user = await agent.Account.login(credentials);
            store.commonStore.setToken(user.token);
            runInAction(() => {
                this.user = user;
            })
            history.push('activities')
            store.modalStore.closeModal();
        } catch (error) {
            throw error;
        }
    }

    logout = () => {
        store.commonStore.setToken(null);
        window.localStorage.removeItem('Token');
        this.user = null;
        history.push('/')
    }

    getUser = async() => {
        try {
            const user = await agent.Account.current();
            runInAction(() => {
                this.user = user;
            })
        } catch (error) {
            console.log(error);
        }
    }

    register = async (credentials: UserFormValues) => {
        try {
            const user = await agent.Account.register(credentials);
            store.commonStore.setToken(user.token);
            runInAction(() => {
                this.user = user;
            })
            history.push('activities')
            store.modalStore.closeModal();
        } catch (error) {
            throw error;
        }
    }
}
