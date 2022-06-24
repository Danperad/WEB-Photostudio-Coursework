import axios from "axios";
import {Answer, Employee} from "../models/Models";

const API_URL = "http://localhost:8888/employee/";

class EmployeeService {
    getFree(date: number, dur: number, service: number) {
        return axios.get(API_URL + 'getfree?start=' + date + '&duration=' + dur + '&service=' + service).then((res) => {
            const data: Answer = res.data;
            if (data.status) {
                return data.answer.employees as Employee[];
            }
            return [];
        }).catch((err) => {
            console.log(err);
            return [];
        })
    }
}

export default new EmployeeService();