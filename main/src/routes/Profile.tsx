import {useEffect, useState} from 'react';
import {Box, Button, Card, CardContent, Grid, Paper, Stack, TextField, Typography} from '@mui/material';
import {useNavigate} from 'react-router-dom';
import {useSelector} from "react-redux";
import {RootState} from "../redux/store";
import {styled} from "@mui/material/styles";
import {Order} from "../models/Order.ts";
import ClientService from "../services/ClientService.ts";
import {format} from "date-fns";

const Item = styled(Paper)(({theme}) => ({
    backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
    ...theme.typography.body2,
    padding: theme.spacing(2),
    textAlign: 'center',
    color: theme.palette.text.secondary,
}));

export default function Profile() {
    const user = useSelector((state: RootState) => state.client);
    const navigate = useNavigate();
    const [key, setKey] = useState<boolean>(false);
    const [orders, setOrders] = useState<Order[]>([])

    useEffect(() => {
        if (key) return;
        if (!user.isAuth) {
            navigate("/")
            return;
        }
        ClientService.getOrders().then(res => {
            setOrders(res);
        })
        setKey(true);
    }, [user]);

    if (user.client === null) {
        return <></>;
    }
    return (
        <div style={{width: "100%"}}>
            <Stack direction="row" spacing={10} sx={{margin: "30px 10px 0 10px"}}>
                <Box sx={{width: "100%"}}>
                    <Typography variant="h5" color="primary" align='center'>Редактирование профиля</Typography>
                    <Stack
                        component="form"
                        sx={{width: '60%', mt: '30px', ml: "20%"}}
                        spacing={2}
                        noValidate
                        autoComplete="off"
                    >
                        <TextField label="Фамилия" variant={"outlined"} size='small' value={user.client.lastName}
                                   required/>
                        <TextField label="Имя" size='small' value={user.client.firstName} required/>
                        <TextField label="Отчество" size='small' value={user.client.middleName}/>
                        <TextField label="Номер телефона" size='small' type={'phone'} value={user.client.phone}
                                   required/>
                        <TextField label="Email" size='small' type={'email'} value={user.client.eMail} required/>
                    </Stack>
                    <Stack direction="row" spacing={2} justifyContent="center" alignItems="center" mt={2} ml={12}>
                        <Button variant="contained" color="secondary" size="medium" disableElevation
                                sx={{borderRadius: '10px'}}>
                            Сохранить
                        </Button>
                    </Stack>
                </Box>
            </Stack>
            <Box mt={3} sx={{backgroundColor: '#F0EDE8', pt: '20px', pb: '20px'}}>
                <Box sx={{flexGrow: 1}} px={1}>
                    <Grid container spacing={{xs: 2, md: 3}} columns={{xs: 4, sm: 8, md: 12}}>
                        {orders.map((order) => (
                            <Grid item xs={2} sm={4} md={4} key={order.number}>
                                <Item sx={{padding: 0, borderRadius: "10px"}}>
                                    <Card sx={{
                                        display: 'flex',
                                        border: "3px solid #776D61",
                                        borderRadius: "10px",
                                        mt: "10px"
                                    }}>
                                        <Box sx={{
                                            display: 'flex',
                                            flexDirection: 'column',
                                            width: '100%',
                                            margin: '0 auto'
                                        }}>
                                            <CardContent sx={{flex: '1 0 auto'}}>
                                                <Typography variant="subtitle1">
                                                    Номер заказа: {order.number}
                                                </Typography>
                                                <Typography variant="subtitle1">
                                                    Дата и время: {format(order.dateTime, "dd-MM-yyyy HH:mm")}
                                                </Typography>
                                                {order.servicePackage && (
                                                    <Stack direction="row">
                                                        <Typography variant="subtitle1">Пакет
                                                            услуг: {order.servicePackage.title}</Typography>
                                                    </Stack>
                                                )}
                                                {order.services.length !== 0 && (
                                                    <Stack direction="row">
                                                        <Typography variant="subtitle1">Услуги: </Typography>
                                                        {order.services.map((service, index) => (
                                                            <Typography>{service.service.title}{order.services.length - 1 > index ? ", " : ""}</Typography>
                                                        ))
                                                        }
                                                    </Stack>
                                                )}
                                                <Stack direction="row" justifyContent="space-between"
                                                       alignItems="center">
                                                    <Typography variant="subtitle1">
                                                        Стоимость: {order.totalPrice}
                                                    </Typography>
                                                </Stack>
                                            </CardContent>
                                        </Box>
                                    </Card>
                                </Item>
                            </Grid>
                        ))}
                    </Grid>
                </Box>
            </Box>
        </div>
    );
}