import axios from "axios";
import {Answer, RentedItem} from "../models/Models";

const API_URL = "http://localhost:8888/renteditems/";

class RentedItemService{
    getFree(date: number, dur: number, service: number) {
        return axios.get(API_URL + 'get?start=' + date + '&duration=' + dur + '&type=' + service).then((res) => {
            const data: Answer = res.data;
            if (data.status) {
                return data.answer.items as RentedItem[];
            }
            return [];
        }).catch((err) => {
            console.log(err);
            return [];
        })
    }
}
export default new RentedItemService();