import axios from '../utils/axiosInstance.ts';
import {Answer, RentedItem} from "../models/Models";

const API_URL = "renteditems/";

class RentedItemService {
  getFree = (date: number, dur: number, service: number) => {
    console.log(service)
    const params = new URLSearchParams();
    params.append('start', new Date(date).toISOString());
    params.append('duration', String(dur));
    params.append('type', String(2));
    return axios.get(API_URL + 'getfree', {params: params}).then((res) => {
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
