import axios from '../utils/axiosInstance.ts';
import authHeader from '../redux/AuthHeader';
import {Answer, NewService, NewServicePackage} from "../models/Models";
import {clientActions} from "../redux/slices/clientSlice";
import errorParser from "../errorParser";
import {snackbarActions} from "../redux/slices/snackbarSlice";

const API_URL = "client/"

interface State {
    serviceModels: NewService[],
    servicePackage?: NewServicePackage
}

interface Serv {
    serviceId: number,
    startDateTime?: number | Date,
    duration?: number,
    hallId?: number,
    employeeId?: number,
    address?: string,
    rentedItemId?: number,
    number?: number,
    isFullTime?: boolean
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

    buy = (cart: State) => {
        const mass: Serv[] = []
        cart.serviceModels.forEach((serv: NewService) => {
            mass.push({
                serviceId: serv.service.id,
                isFullTime: serv.isFullTime,
                employeeId: serv.employee ? serv.employee!.id : undefined,
                hallId: serv.hall ? serv.hall!.id : undefined,
                startDateTime: serv.startDateTime,
                number: serv.number,
                duration: serv.duration,
                rentedItemId: serv.rentedItem ? serv.rentedItem!.id : undefined
            })
        })
        const req = {
            serviceModels: mass,
            servicePackage: cart.servicePackage === undefined ? null : {
                id: cart.servicePackage!.service.id,
                startTime: cart.servicePackage!.startTime
            }
        };
        return axios.post(API_URL + 'buy', req, {headers: authHeader()}).then((res) => {
            const data: Answer = res.data;
            if (data.status) {
                return true;
            }
            console.log(data);
            return false;
        }).catch((err) => {
            console.log(err);
            return false;
        })
    };

    getOrders = () => {
        return axios.get(API_URL + "orders", {headers: authHeader()}).then((res) => {
            const data: Answer = res.data
            if (!data.status) {
                return snackbarActions.ErrorAction("Не удалось загшрузить заявки");
            }
            return data.answer
        });
    }
}

export default new ClientService();