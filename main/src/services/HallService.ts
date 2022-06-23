import axios from "axios";
import {Answer, HallModel} from "../models/Models";
import errorParser from "../errorParser";
import {snackbarActions} from "../redux/slices/snackbarSlice";
import {HallsLoaded} from "../redux/actions/hallActions";

const API_URL = "http://localhost:8888/hall/"
class HallService {
    getHalls() {
        return axios.get(API_URL + 'get')
            .then((res) => {
                const data: Answer = res.data;
                if (data.status){
                    return HallsLoaded(data.answer.halls as HallModel[]);
                }
                const error = errorParser(String(data.error!));
                return snackbarActions.ErrorAction(error);
            }).catch((err) => {
                return snackbarActions.ErrorAction(err.message);
            })
    }
}

export default new HallService();