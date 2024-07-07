import axios from '../utils/axiosInstance.ts';
import {Answer, Hall} from "../models/Models";

const API_URL = "hall/"

class HallService {
  getFree(date: Date, dur: number) {
    const params = new URLSearchParams();
    params.append('start', new Date(date).toISOString());
    params.append('duration', String(dur));
    return axios.get(API_URL + 'getfree', {params: params}).then((res) => {
      const data: Answer = res.data;
      if (data.status) {
        return data.answer.halls as Hall[];
      }
      return [];
    }).catch((err) => {
      console.log(err);
      return [];
    })
  }
}

export default new HallService();
