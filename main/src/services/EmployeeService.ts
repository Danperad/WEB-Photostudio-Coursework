import axios from '../utils/axiosInstance.ts';
import {Answer, Employee} from "../models/Models";

const API_URL = "employee/";

class EmployeeService {
    getFree = (date: number, dur: number, service: number) => {
        const params = new URLSearchParams();
        params.append('start', String(new Date(date).toISOString()));
        params.append('duration', String(dur));
        params.append('service', String(service));
        return axios.get(API_URL + 'getfree', {params: params}).then((res) => {
            const data: Answer = res.data;
            if (data.status) {
                return data.answer.employees as Employee[];
            }
            return [];
        }).catch((err) => {
            console.log(err);
            return [];
        })
    };
}

export default new EmployeeService();