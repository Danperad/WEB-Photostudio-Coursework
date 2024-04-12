import axios from '../utils/axiosInstance.ts';
import authHeader from '../redux/AuthHeader';
import {Answer, NewService, NewServicePackage} from "../models/Models";
import {clientActions} from "../redux/slices/clientSlice";
import errorParser from "../errorParser";
import {snackbarActions} from "../redux/slices/snackbarSlice";

const API_URL = "client/"

interface State {
    serviceModels: NewService[],
    servicePackage: NewServicePackage | null
}

interface Serv {
    serviceId: number,
    startDateTime: number | null,
    duration: number | null,
    hallId: number | null,
    employeeId: number | null,
    address: string | null,
    rentedItemId: number | null,
    number: number | null,
    isFullTime: boolean | null
}

class ClientService {
    loadClient = () => {
        const header = authHeader();
        if (!header.Authorization)
            return undefined
        return axios.get(API_URL + "get", {headers: authHeader()})
            .then((res) => {
                const data: Answer = res.data;
                if (data.status) {
                    return clientActions.clientLoaded(data.answer.user);
                }
                errorParser(String(data.error!));
                return clientActions.logout();
            }).catch((err) => {
                console.log(err);
                return snackbarActions.ErrorAction(err.message);
            })
    };

    updateAvatar = (avatar: string) => axios.post(API_URL + 'avatar', {avatar: avatar}, {headers: authHeader()})
        .then((res) => {
            const data: Answer = res.data;
            if (data.status) {
                return clientActions.addedAvatar(data.answer.user);
            }
            const error = errorParser(String(data.error!));
            return snackbarActions.ErrorAction(error);
        }).catch((err) => {
            return snackbarActions.ErrorAction(err.message);
        });

    buy = (cart: State) => {
        const mass: Serv[] = []
        cart.serviceModels.forEach((serv) => {
            mass.push({
                serviceId: serv.service.id,
                isFullTime: serv.isFullTime,
                employeeId: serv.employee === null ? null : serv.employee!.id,
                hallId: serv.hall === null ? null : serv.hall!.id,
                startDateTime: serv.startTime,
                number: serv.number,
                duration: serv.duration,
                address: serv.address,
                rentedItemId: serv.rentedItem === null ? null : serv.rentedItem!.id
            })
        })
        const req = {
            serviceModels: mass,
            servicePackage: cart.servicePackage === null ? null : {id: cart.servicePackage!.service.id, startTime: cart.servicePackage!.startTime}
        };
        return axios.post(API_URL + 'buy', req, {headers: authHeader()}).then((res) => {
            const data: Answer = res.data;
            if (data.status){
                return true;
            }
            console.log(data);
            return false;
        }).catch((err) => {
            console.log(err);
            return false;
        })
    };
}

export default new ClientService();