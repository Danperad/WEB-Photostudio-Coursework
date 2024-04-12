import axios from '../utils/axiosInstance.ts';
import {Answer, RentedItem} from "../models/Models";

const API_URL = "renteditems/";

class RentedItemService{
    getFree = (date: number, dur: number, service: number) => {
        const params = new URLSearchParams();
        params.append('start', new Date(date).toISOString());
        params.append('duration', String(dur));
        params.append('type', String(service));
        return axios.get(API_URL + 'get?start=' + date + '&duration=' + dur + '&type=' + service, {params: params}).then((res) => {
            const data: Answer = res.data;
            if (data.status) {
                return data.answer.items as RentedItem[];
            }
            return [];
        }).catch((err) => {
            console.log(err);
            return [];
        })
    };
}
export default new RentedItemService();