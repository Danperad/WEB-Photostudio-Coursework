import axios from 'axios';
import authHeader from '../AuthHeader';
import {Answer} from "../../models/RequestModels";
import {AvatarError, AddedAvatar} from '../actions/clientActions'
const API_URL = "http://localhost:8888/client/"

class ClientService {
	updateAvatar(avatar: string) {
		return axios.post(API_URL + 'avatar', {avatar: avatar}, {headers: authHeader()})
			.then((res) => {
				const data: Answer = res.data;
				if (data.status){
					return AddedAvatar(data.answer.user.avatar);
				}
				return AvatarError(data.errorText!);
		}).catch((err) => {
			return AvatarError(err);
			})
	}
}

export default new ClientService();