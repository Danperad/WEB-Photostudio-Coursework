import axios from "axios";
import {Answer, Service} from "../models/Models";
import {serviceActions} from "../redux/slices/serviceSlice";
import errorParser from "../errorParser";
import {snackbarActions} from "../redux/slices/snackbarSlice";

const API_URL = "http://localhost:8888/services/"

class ServicesService {
    getServices(search: string, sort: string, type: string, start: number) {
        return axios.get(API_URL + 'get?search=' + search + '&order=' + sort + '&type=' + type + '&count=6&start=' + (start + 1))
            .then((res) => {
                const data: Answer = res.data;
                if (data.status) {
                    return serviceActions.ServicesLoaded({
                        services: data.answer.services as Service[],
                        hasMore: data.answer.hasMore
                    });
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