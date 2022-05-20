import axios from 'axios';
import authHeader from '../redux/AuthHeader';
import {Answer} from "../models/Models";
import {AvatarError} from '../redux/actions/clientActions'
import {clientActions} from "../redux/slices/clientSlice";
const API_URL = "http://localhost:8888/client/"

class ClientService {
	updateAvatar(avatar: string) {
		return axios.post(API_URL + 'avatar', {avatar: avatar}, {headers: authHeader()})
			.then((res) => {
				const data: Answer = res.data;
				if (data.status){
					localStorage.setItem('user', JSON.stringify(data.answer.user))
					return clientActions.addedAvatar(data.answer.user);
				}
				return AvatarError(data.errorText!);
		}).catch((err) => {
			return AvatarError(err);
			})
	}
}

export default new ClientService();