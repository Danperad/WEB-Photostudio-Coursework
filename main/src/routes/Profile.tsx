import React, {useEffect} from 'react';
import {Stack, Typography, Button, CardMedia} from "@mui/material";
import {useDispatch, useSelector} from "react-redux";
import {AppDispatch, RootState} from "../redux/store";
import {useNavigate} from "react-router-dom";
import ClientService from '../redux/services/ClientService'

function Profile() {
	const user = useSelector((state: RootState) => state);
	const dispatch = useDispatch<AppDispatch>();
	const navigate = useNavigate();
	const [avatar, setAvatar] = React.useState<string | undefined>(undefined);
	useEffect(() => {
		if (!user.client.isAuth) {
			navigate("/")
		}
	}, [user, navigate]);

	const onChange = (event: any) => {
		const file:File = event.target.files[0] ;
		if (file === undefined) return;
		let reader = new FileReader();
		reader.onloadend = function (){
			setAvatar(reader.result!.toString());
			console.log(reader.result!.toString())
		}
		reader.onerror = function (err) {
			console.log(err);
		}
		reader.readAsDataURL(file);
	}
	const onClick = (event: any) => {
		if (avatar === "" || avatar === undefined) return;
		ClientService.updateAvatar(avatar!).then((res) => {
			dispatch(res);
		})
	}
	return (
		<Stack>
			{user.client.isAuth &&
          <>
              <Typography color={"white"}>Фамилия: {user.client.client!.lastName}</Typography>
              <Typography color={"white"}>Имя: {user.client.client!.firstName}</Typography>
						{user.client.client!.middleName !== null &&
                <Typography color={"white"}>Отчество: {user.client.client!.middleName}</Typography>
						}
              <Typography color={"white"}>Почта: {user.client.client!.email}</Typography>
              <Typography color={"white"}>Телефон: {user.client.client!.phone}</Typography>
              <Typography color={"white"}>Логин: {user.client.client!.login}</Typography>
						{user.client.client!.company !== null &&
                <Typography color={"white"}>Компания: {user.client.client!.company}</Typography>
						}
						{avatar !== "" &&
                <CardMedia component={"img"} src={avatar}/>
						}
              <Button variant="contained" component="label">Upload File
                  <input type="file" accept="image/*" hidden onChange={onChange}/>
              </Button>
							<Button onClick={onClick}>Обновить аватар</Button>
          </>
			}
		</Stack>
	);
}

export default Profile;