import axios from '../utils/axiosInstance.ts';
import {getCookie, removeCookie, setCookie} from "typescript-cookie";
import {Answer, Client, LoginModel, RegistrationModel} from "../models/Models";
import {clientActions} from '../redux/slices/clientSlice'
import errorParser from "../errorParser";
import {snackbarActions} from "../redux/slices/snackbarSlice";

const API_URL = "auth/"

class AuthService {
    register = (reg: RegistrationModel) => axios.post(API_URL + "signup", reg)
        .then((res) => {
            const data: Answer = res.data;
            if (data.status) {
                setCookie("access_token", data.answer.accessToken, {expires: 1, path: ''});
                setCookie("refresh_token", data.answer.refreshToken, {expires: 90, path: ''});
                const client: Client = data.answer.user;
                return clientActions.registerSuccess(client);
            }
            const error = errorParser(String(data.error!));
            return snackbarActions.ErrorAction(error);
        }).catch((err) => {
            return snackbarActions.ErrorAction(err.message);
        });

    login = (login: LoginModel) => axios.post(API_URL + "signin", login).then(
        (res) => {
            const data: Answer = res.data;
            if (data.status) {
                setCookie("access_token", data.answer.accessToken, {expires: 1, path: ''});
                setCookie("refresh_token", data.answer.refreshToken, {expires: 90, path: ''});
                const client: Client = data.answer.user;
                return clientActions.loginSuccess(client);
            }
            const error = errorParser(String(data.error!));
            return snackbarActions.ErrorAction(error);
        }).catch((err) => {
        return snackbarActions.ErrorAction(err.message);
    });

    reAuth() {
        if (!getCookie("refresh_token")) return;
        const params = new URLSearchParams();
        params.append('token', getCookie("refresh_token")!)
        axios.get(API_URL + "reauth", {params: params}).then(
            (res) => {
                const data: Answer = res.data;
                if (!data.status) return;
                setCookie("access_token", data.answer.accessToken, {expires: 1, path: ''});
                setCookie("refresh_token", data.answer.refreshToken, {expires: 90, path: ''});
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