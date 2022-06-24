import React, {useState} from 'react';
import {
    Button,
    Stack,
    TextField,
    Card,
    FormControlLabel,
    FormGroup,
    Checkbox,
    Typography
} from "@mui/material";
import IconButton from '@mui/material/IconButton';
import DeleteOutlineOutlinedIcon from '@mui/icons-material/DeleteOutlineOutlined';
import {useDispatch, useSelector} from "react-redux";
import {AppDispatch, RootState} from "../redux/store";
import {cartActions} from "../redux/slices/cartSlice";
import {OpenModal} from "../redux/actions/authModalActions";
import ClientService from "../services/ClientService";

export default function ShoppingCart() {
    const [dateTime, setDateTime] = useState<string>();
    const state = useSelector((state: RootState) => state);
    const dispatch = useDispatch<AppDispatch>();

    const cartLength = () => {
        return state.cart.servicePackage !== null ? state.cart.serviceModels.length + 1 : state.cart.serviceModels.length
    }
    const buy = (e: any) => {
        if (!state.client.isAuth) {
            dispatch(OpenModal(true));
            return;
        }
        ClientService.buy(state.cart).then((res) => {
            if (res) dispatch(cartActions.ClearCart(0));
        })
    }
    return (
        <div className='section'
             style={{backgroundColor: '#F0EDE8', borderRadius: '20px', padding: '22px', width: '100%', height: '100%'}}>
            <Stack direction="row" spacing={8} alignItems='center' justifyContent="center">
                <Typography variant="h6" color="primary" align='center'>Корзина</Typography>
                <Stack direction="row" spacing={2}>
                    <Typography variant="h6" color="primary" align='center'>Выбрано: {cartLength()}</Typography>
                    <Button variant="contained" color="secondary" size="medium" disableElevation
                            disabled={cartLength() === 0} onClick={buy}
                            sx={{borderRadius: '10px'}}>
                        Оплатить
                    </Button>
                </Stack>
            </Stack>

            <Stack spacing={2} sx={{width: "65%", ml: "18%"}} alignItems="center" mt={2}>
                {state.cart.servicePackage != null &&
                    <Card sx={{
                        display: 'flex',
                        border: "3px solid #776D61",
                        borderRadius: "10px",
                        mt: "10px",
                        width: "80%"
                    }}>
                        <Stack direction="row" spacing={8} mt={2} ml={2} mb={2} sx={{width: "100%"}}>
                            <Stack spacing={2} sx={{width: "45%"}}>
                                <Typography color={'primary'}
                                            fontWeight={'bold'}>{state.cart.servicePackage!.service.title}</Typography>
                                <TextField label="Адрес" color='primary' size='small' disabled/>
                                <TextField
                                    id="datetime-local"
                                    label="Дата и время"
                                    type="datetime-local"
                                    defaultValue={dateTime}
                                    onChange={e => {
                                        setDateTime(e.target.value);
                                    }}
                                    InputLabelProps={{
                                        shrink: true,
                                    }} disabled
                                />
                                <TextField label="Продолжительность" color='primary' size='small' disabled/>
                            </Stack>
                            <Stack spacing={2}>
                                <TextField label="Цена" color='primary' size='small' disabled/>
                                <Typography>Стилист</Typography>
                                <FormGroup>
                                    <FormControlLabel control={<Checkbox defaultChecked disabled/>}
                                                      label="На все время?"/>
                                </FormGroup>
                                <TextField label="Выбранный предмет" color='primary' size='small' disabled/>
                            </Stack>
                            <Stack spacing={1} direction={'row'} height={10}>
                                <IconButton aria-label="delete" onClick={() => {
                                    dispatch(cartActions.PackageRemoved(state.cart.servicePackage!))
                                }}>
                                    <DeleteOutlineOutlinedIcon/>
                                </IconButton>
                            </Stack>
                        </Stack>
                    </Card>
                }
                {state.cart.serviceModels.map((service, index) => (
                    <Card
                        sx={{
                            display: 'flex',
                            border: "3px solid #776D61",
                            borderRadius: "10px",
                            mt: "10px",
                            width: "80%"
                        }} key={index}>
                        <Stack direction="row" spacing={8} mt={2} ml={2} mb={2} sx={{width: "100%"}}>
                            <Stack spacing={2} sx={{width: "45%"}}>
                                <Typography color={'primary'} fontWeight={'bold'}>{service.service.title}</Typography>
                                <TextField label="Адрес" color='primary' size='small' disabled/>
                                <TextField
                                    id="datetime-local"
                                    label="Дата и время"
                                    type="datetime-local"
                                    defaultValue={dateTime}
                                    onChange={e => {
                                        setDateTime(e.target.value);
                                    }}
                                    InputLabelProps={{
                                        shrink: true,
                                    }} disabled
                                />
                                <TextField label="Продолжительность" color='primary' size='small' disabled/>
                            </Stack>
                            <Stack spacing={2}>
                                <TextField label="Цена" color='primary' size='small' disabled/>
                                <Typography>Стилист</Typography>
                                <FormGroup>
                                    <FormControlLabel control={<Checkbox defaultChecked disabled/>}
                                                      label="На все время?"/>
                                </FormGroup>
                                <TextField label="Выбранный предмет" color='primary' size='small' disabled/>
                            </Stack>
                            <Stack spacing={1} direction={'row'} height={10}>
                                <IconButton aria-label="delete" onClick={() => {
                                    dispatch(cartActions.ServiceRemoved(service))
                                }}>
                                    <DeleteOutlineOutlinedIcon/>
                                </IconButton>
                            </Stack>
                        </Stack>
                    </Card>
                ))}
            </Stack>
        </div>
    )
}
