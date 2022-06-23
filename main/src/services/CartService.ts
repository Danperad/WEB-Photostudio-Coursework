import axios from "axios";
import {Answer} from "../models/Models";

const API_URL = "http://localhost:8888/cart/"

class CartService{
    checkAvailable(serviceId: number, cart: any){
        return axios.post(API_URL + 'checkservice?id='+serviceId, cart).then((res) => {
            const data : Answer = res.data;
            if (data.status){
                console.log(data.answer as boolean);
                return data.answer as boolean;
            }
            return false;
        }).catch((err) => {
            return false;
        })
    }
}

export default new CartService();