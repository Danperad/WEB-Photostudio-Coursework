import axios from "axios";
import {Answer, ServiceModel} from "../models/Models";
import {ServicesLoaded} from "../redux/actions/serviceActions";
import errorParser from "../errorParser";
import {snackbarActions} from "../redux/slices/snackbarSlice";

const API_URL = "http://localhost:8888/services/"

class ServicesService {
    getServices(search: string, sort: string, type: string) {
        return axios.get(API_URL + 'get?search='+search + '&order=' + sort + '&type='+type)
            .then((res) => {
                const data: Answer = res.data;
                if (data.status) {
                    return ServicesLoaded(data.answer.services as ServiceModel[]);
                }
                const error = errorParser(String(data.error!));
                return snackbarActions.ErrorAction(error);
            }).catch((err) => {
                console.log(err.message)
                return snackbarActions.ErrorAction(err.message);
            })
    }

    getServiceById(id: number) {
        return axios.get(API_URL + 'getId?id=')
            .then((res) => {
                const data: Answer = res.data;
                if (data.status) {
                    return data.answer.service;
                }
                return null;
            }).catch((err) => {
                return snackbarActions.ErrorAction(err.message);
            })
    }
}

export default new ServicesService();