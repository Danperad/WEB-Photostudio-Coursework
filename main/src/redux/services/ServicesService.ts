import axios from "axios";
import {Answer} from "../../models/RequestModels";
import {ServiceModel} from "../../models/ServiceModel";

const API_URL = "http://localhost:8888/services/"
class ServicesService {
	getServices() {
		return axios.get(API_URL + 'get')
			.then((res) => {
				const data: Answer = res.data;
				if (data.status){
					return data.answer.services as ServiceModel[];
				}
				return [];
			}).catch((err) => {
				return [];
			})
	}
}

export default new ServicesService();