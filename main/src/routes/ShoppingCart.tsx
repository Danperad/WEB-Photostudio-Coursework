import {Button, Card, Checkbox, FormControlLabel, FormGroup, Stack, TextField, Typography} from "@mui/material";
import IconButton from '@mui/material/IconButton';
import DeleteOutlineOutlinedIcon from '@mui/icons-material/DeleteOutlineOutlined';
import {useDispatch, useSelector} from "react-redux";
import {AppDispatch, RootState} from "../redux/store";
import {cartActions} from "../redux/slices/cartSlice";
import {OpenModal} from "../redux/actions/authModalActions";
import ClientService from "../services/ClientService";
import {snackbarActions} from "../redux/slices/snackbarSlice.ts";
import {ServiceType} from "../models/ServiceModel.ts";
import NewServiceModel from "../models/NewServiceModel.ts";
import {format} from "date-fns";

export default function ShoppingCart() {
    const state = useSelector((state: RootState) => state);
    const dispatch = useDispatch<AppDispatch>();

    const cartLength = () => {
        return state.cart.servicePackage !== undefined ? state.cart.serviceModels.length + 1 : state.cart.serviceModels.length
    }
    const buy = () => {
        if (!state.client.isAuth) {
            dispatch(OpenModal(true));
            return;
        }
        ClientService.buy(state.cart).then((res) => {
            if (res) {
                dispatch(cartActions.ClearCart());
                dispatch(snackbarActions.okAction("Заявка успешно добавлена"))
            }
        })
    }

    const getPrice = (service: NewServiceModel): number => {
        let price = service.service.cost;
        switch (service.service.type) {
            case ServiceType.ItemRent:
                price += ((service.number!) * service.rentedItem!.cost) * (service.duration!) / 60;
                break
            case ServiceType.HallRent:
                price += service.hall!.pricePerHour * (service.duration!) / 60;
                break
            case ServiceType.Photo:
            case ServiceType.Style:

                price += service.employee!.cost * (service.duration!) / 60;
                break
        }
        return price;
    }

    const selectedItemInService = (service: NewServiceModel): string | undefined => {
        switch (service.service.type) {
            case ServiceType.ItemRent:
                return `${service.rentedItem!.title} (${service.number!} шт.)`
            case ServiceType.HallRent:
                return `${service.hall!.title}`

            case ServiceType.Photo:
            case ServiceType.Style:
                return `${service.employee!.lastName} ${service.employee!.firstName.charAt(0)}.${service.employee!.middleName ? " " + service.employee!.middleName.charAt(0) + "." : ""}`
        }
        return undefined
    }

    const totalPrice = () => {
        let totalPrice = 0;
        if (state.cart.servicePackage) {
            totalPrice += state.cart.servicePackage.service.cost;
        }
        state.cart.serviceModels.forEach(s => {
            totalPrice += getPrice(s)
        })
        return totalPrice
    }

    return (
        <div className='section'
             style={{backgroundColor: '#F0EDE8', borderRadius: '20px', padding: '22px', width: '100%', height: '100%'}}>
            <Stack direction="row" spacing={8} alignItems='center' justifyContent="center">
                <Typography variant="h6" color="primary" align='center'>Корзина</Typography>
                <Stack direction="row" spacing={2}>
                    <Typography variant="h6" color="primary" align='center'>Выбрано: {cartLength()}</Typography>
                    <Typography variant="h6" color="primary" align='center'>Общая
                        стоимость: {totalPrice()} руб.</Typography>
                    <Button variant="contained" color="secondary" size="medium" disableElevation
                            disabled={cartLength() === 0} onClick={buy}
                            sx={{borderRadius: '10px'}}>
                        Оплатить
                    </Button>
                </Stack>
            </Stack>

            <Stack spacing={2} sx={{width: "65%", ml: "18%"}} alignItems="center" mt={2}>
                {state.cart.servicePackage &&
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
                                            fontWeight={'bold'}>Пакет
                                    услуг: {state.cart.servicePackage!.service.title}</Typography>
                                <TextField
                                    label="Дата и время"
                                    defaultValue={format(new Date(state.cart.servicePackage!.startTime), "dd-MM-yyyy HH:mm")}
                                    InputLabelProps={{
                                        shrink: true,
                                    }} inputProps={{
                                    readOnly: Boolean(true),
                                    unselectable: "on"
                                }}
                                />
                            </Stack>
                            <Stack spacing={2}>
                                <TextField label="Цена" color='primary' size='small' inputProps={{
                                    readOnly: Boolean(true),
                                    unselectable: "on"
                                }}
                                           defaultValue={state.cart.servicePackage!.service.cost}/>
                            </Stack>
                            <Stack spacing={1} direction={'row'} height={10}>
                                <IconButton aria-label="delete" onClick={() => {
                                    dispatch(cartActions.PackageRemoved())
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
                                {service.startDateTime && (
                                    <TextField
                                        label="Дата и время"
                                        defaultValue={format(new Date(service.startDateTime!), "dd-MM-yyyy HH:mm")}
                                        InputLabelProps={{
                                            shrink: true,
                                        }}
                                        inputProps={{
                                            readOnly: Boolean(true),
                                            unselectable: "on"
                                        }}
                                    />
                                )}
                                {service.duration && (
                                    <TextField inputProps={{
                                        readOnly: Boolean(true),
                                        unselectable: "on"
                                    }} label="Продолжительность" color='primary' size='small'
                                               defaultValue={service.duration}/>
                                )}
                            </Stack>
                            <Stack spacing={2}>
                                <TextField label="Цена" color='primary' size='small' disabled
                                           defaultValue={getPrice(service)}/>
                                {service.isFullTime !== undefined && (
                                    <FormGroup>
                                        <FormControlLabel control={<Checkbox checked={service.isFullTime}/>}
                                                          label="На все время?"/>
                                    </FormGroup>
                                )}
                                {service.service.type !== ServiceType.Simple && (
                                    <TextField label="Выбранный предмет" color='primary' size='small' inputProps={{
                                        readOnly: Boolean(true),
                                        unselectable: "on"
                                    }} defaultValue={selectedItemInService(service)}/>
                                )}
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
