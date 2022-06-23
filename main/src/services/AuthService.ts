import axios from 'axios';
import {getCookie, removeCookie, setCookie} from "typescript-cookie";
import {Answer, LoginModel, RegistrationModel, Client} from "../models/Models";
import {clientActions} from '../redux/slices/clientSlice'
import errorParser from "../errorParser";
import {snackbarActions} from "../redux/slices/snackbarSlice";

const API_URL = "http://localhost:8888/auth/"

class AuthService {
    register(reg: RegistrationModel) {
        return axios.post(API_URL + "signup", reg)
            .then((res) => {
                const data: Answer = res.data;
                if (data.status) {
                    setCookie("access_token", data.answer.access_token, {expires: 1, path: ''});
                    setCookie("refresh_token", data.answer.refresh_token, {expires: 90, path: ''});
                    const client: Client = data.answer.user;
                    return clientActions.registerSuccess(client);
                }
                const error = errorParser(String(data.error!));
                return snackbarActions.ErrorAction(error);
            }).catch((err) => {
                return snackbarActions.ErrorAction(err.message);
            })
    }

    login(login: LoginModel) {
        return axios.post(API_URL + "signin", login).then(
            (res) => {
                const data: Answer = res.data;
                if (data.status) {
                    setCookie("access_token", data.answer.access_token, {expires: 1, path: ''});
                    setCookie("refresh_token", data.answer.refresh_token, {expires: 90, path: ''});
                    const client: Client = data.answer.user;
                    return clientActions.loginSuccess(client);
                }
                const error = errorParser(String(data.error!));
                return snackbarActions.ErrorAction(error);
            }).catch((err) => {
            return snackbarActions.ErrorAction(err.message);
        })
    }

    reAuth() {
		if (getCookie("refresh_token") === undefined) return;
        axios.get(API_URL + "reauth?token=" + getCookie("refresh_token")).then(
            (res) => {
                const data: Answer = res.data;
                if (!data.status) return;
				setCookie("access_token", data.answer.access_token, {expires: 1, path: ''});
				setCookie("refresh_token", data.answer.refresh_token, {expires: 90, path: ''});
			}).catch((err) => {
            	console.log(err.message)
        })
    }

    logout() {
        removeCookie("access_token", {path: ''});
        removeCookie("refresh_token", {path: ''});
        return clientActions.logout();
    }
}

export default new AuthService();