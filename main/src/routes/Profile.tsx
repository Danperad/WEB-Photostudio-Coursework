import {useEffect, useState} from 'react';
import {styled} from '@mui/material/styles';
import {Box, Button, Paper, Stack, TextField, Typography} from '@mui/material';
import {useNavigate} from 'react-router-dom';
import {useSelector} from "react-redux";
import {RootState} from "../redux/store";

// @ts-ignore
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

    useEffect(() => {
        if (key) return;
        if (!user.isAuth) {
            navigate("/")
        }
        setKey(true);
    }, [user]);

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
                        <TextField label="Фамилия" variant={"outlined"} size='small' value={user.client?.lastName!} required/>
                        <TextField label="Имя" size='small' value={user.client?.firstName!} required/>
                        <TextField label="Отчество" size='small' value={user.client?.middleName!}/>
                        <TextField label="Номер телефона" size='small' type={'phone'} value={user.client?.phone!} required/>
                        <TextField label="Email" size='small' type={'email'} value={user.client?.eMail!} required/>
                    </Stack>
                    <Stack direction="row" spacing={2} justifyContent="center" alignItems="center" mt={2} ml={12}>
                        <Button variant="contained" color="secondary" size="medium" disableElevation
                                sx={{borderRadius: '10px'}}>
                            Выбрать аватар
                        </Button>
                        <Button variant="contained" color="secondary" size="medium" disableElevation
                                sx={{borderRadius: '10px'}}>
                            Сохранить
                        </Button>
                    </Stack>
                </Box>
                <img
                    src={user.client?.avatar}
                    alt="avatar"
                    width="100%"
                    height="100%"
                />
            </Stack>
            <Box mt={3} sx={{backgroundColor: '#F0EDE8', pt: '20px', pb: '20px'}}>
                <Stack direction="row" spacing={2} alignItems="center" ml={4}>
                    <TextField
                        id="datetime-local"
                        label="Дата и время"
                        type="datetime-local"
                        InputLabelProps={{
                            shrink: true,
                        }}
                    />
                    <TextField
                        id="datetime-local"
                        label="Дата и время"
                        type="datetime-local"
                        InputLabelProps={{
                            shrink: true,
                        }}
                    />
                    <Button variant="contained" color="secondary" size="medium" disableElevation
                            sx={{borderRadius: '10px', height: "40px"}}>
                        Сформировать отчет
                    </Button>
                </Stack>
                {/*
                <Box sx={{flexGrow: 1}}>
                    <Grid container spacing={{xs: 2, md: 3}} columns={{xs: 4, sm: 8, md: 12}}>
                        {Array.from(Array(2)).map((_, index) => (
                            <Grid item xs={2} sm={4} md={4} key={index}>
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
                                                    Номер заказа:
                                                </Typography>
                                                <Typography variant="subtitle1">
                                                    Дата и время:
                                                </Typography>
                                                <Stack direction="row">
                                                    <Typography variant="subtitle1">Услуги:</Typography>
                                                     map
                                                    <Typography>услуга1, </Typography>
                                                </Stack>
                                                <Stack direction="row" justifyContent="space-between"
                                                       alignItems="center">
                                                    <Typography variant="subtitle1">
                                                        Стоимость:
                                                    </Typography>
                                                    <Rating name="simple-controlled"/>
                                                </Stack>
                                            </CardContent>
                                        </Box>
                                    </Card>
                                </Item>
                            </Grid>
                        ))}
                    </Grid>
                </Box>
*/}
            </Box>
        </div>
    );
}