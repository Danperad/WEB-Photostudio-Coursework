import axios from '../utils/axiosInstance.ts';
import {Answer, Service} from "../models/Models";
import {serviceActions} from "../redux/slices/serviceSlice";
import errorParser from "../errorParser";
import {snackbarActions} from "../redux/slices/snackbarSlice";

const API_URL = "services/"

class ServicesService {
  getServices(search: string, type: string, start: number) {
    const params = new URLSearchParams();
    if (search !== "")
      params.append("search", search)
    if (type !== "" && +type !== 0)
      params.append("type", type)
    params.append("start", String(start + 1))
    params.append("count", String(6))
    return axios.get(API_URL + 'get', {params: params})
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
}

export default new ServicesService();
