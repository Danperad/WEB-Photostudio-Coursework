import axios from '../utils/axiosInstance.ts';
import {snackbarActions} from "../redux/slices/snackbarSlice";
import {Answer, ServicePackage} from "../models/Models";
import errorParser from "../errorParser";
import {ServicesPackageLoaded} from "../redux/actions/packagesActions";

const API_URL = "package/"

class PackageService {
    getServices(){
        return axios.get(API_URL + 'get').then((res) => {
            const data: Answer = res.data;
            if (data.status){
                console.log(data.answer.packages);
                return ServicesPackageLoaded(data.answer.packages as ServicePackage[]);
            }
            const error = errorParser(String(data.error!));
            return snackbarActions.ErrorAction(error);
        }).catch((err) => {
            console.log(err.message)
            return snackbarActions.ErrorAction(err.message);
        })
    }
}

export default new PackageService();