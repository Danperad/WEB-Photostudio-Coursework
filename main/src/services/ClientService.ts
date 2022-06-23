import axios from 'axios';
import authHeader from '../redux/AuthHeader';
import {Answer} from "../models/Models";
import {clientActions} from "../redux/slices/clientSlice";
import errorParser from "../errorParser";
import {snackbarActions} from "../redux/slices/snackbarSlice";

const API_URL = "http://localhost:8888/client/"

class ClientService {
    loadClient() {
        return axios.get(API_URL + "get", {headers: authHeader()})
            .then((res) => {
                const data: Answer = res.data;
                if (data.status) {
                    return clientActions.clientLoaded(data.answer.user);
                }
                errorParser(String(data.error!));
                return clientActions.logout();
            }).catch((err) => {
                return clientActions.logout();
            })
    }

    updateAvatar(avatar: string) {
        return axios.post(API_URL + 'avatar', {avatar: avatar}, {headers: authHeader()})
            .then((res) => {
                const data: Answer = res.data;
                console.log(data)
                if (data.status) {
                    return clientActions.addedAvatar(data.answer.user);
                }
                const error = errorParser(String(data.error!));
                return snackbarActions.ErrorAction(error);
            }).catch((err) => {
                return snackbarActions.ErrorAction(err.message);
            })
    }
}

export default new ClientService();