import React, {useState} from 'react';
import {Employee, NewServicePackage, ServicePackage} from "../models/Models";
import {
    Box, Button,
    FormControl,
    InputLabel, MenuItem, Modal, Select,
    Stack,
    TextField,
    Typography, SelectChangeEvent
} from "@mui/material";
import ICostable from "../models/ICostable";
import EmployeeService from "../services/EmployeeService";
import {useDispatch} from "react-redux";
import {AppDispatch} from "../redux/store";
import {cartActions} from "../redux/slices/cartSlice";

interface ServicePackageModalProps {
    open: boolean,
    handlerClose: () => void,
    service: ServicePackage | null
}

export default function AddServicePackageModal(props: ServicePackageModalProps) {
    const dispatch = useDispatch<AppDispatch>();

    const [dateTime, setDateTime] = useState<string>('');
    const [items, setItems] = useState<Employee[]>([]);
    const [isEnabled, setEnabled] = useState<boolean>(false);
    const [selectedItem, setSelected] = useState<string>('');
    const [fullEnabled, setFullEnabled] = useState<boolean>(false);

    const check = (date: number) => {
        if (date < +Date.now()) {
            setEnabled(false);
            setItems([]);
            setSelected('');
            setFullEnabled(false);
            return;
        }
        fillItems(date);
    }
    const fillItems = (date: number) => {
        EmployeeService.getFree(date, props.service!.duration, 1).then((res) => {
            if (res.length === 0) return;
            setItems(res);
            setEnabled(true);
        });
    }
    const style = {
        position: 'absolute' as 'absolute',
        top: '50%',
        left: '50%',
        transform: 'translate(-50%, -50%)',
        width: '60%',
        bgcolor: '#F0EDE8',
        border: '2px solid #776D61',
        borderRadius: '20px',
        boxShadow: 24,
        p: 4,
    };

    const handleSelect = (event: SelectChangeEvent) => {
        setSelected(event.target.value as string);
        if ((event.target.value as string) === '') {
            setFullEnabled(false)
            return;
        }
        setFullEnabled(true);
    };

    const getPrice = () => {
        let price = props.service!.cost;
        let item : Employee | null = null;
        items.forEach((i) => {
            if (i.id === +selectedItem) item = i;
        });
        if (item !== null) {
            price += (item as Employee)!.cost;
        }
        return price;
    }

    const buy = () => {
        let empl: Employee | null = null;
        items.forEach((i) => {
            if (i.id === +selectedItem) empl = i;
        });
        const newService: NewServicePackage = {
            service: props.service!,
            startTime: +new Date(dateTime),
            employee: empl!
        }
        dispatch(cartActions.PackageAdded(newService));
        props.handlerClose();
    }

    if (props.service === null) return <></>;
    return (
        <Modal
            open={props.open}
            onClose={props.handlerClose}
            aria-labelledby="modal-modal-title"
            aria-describedby="modal-modal-description"
        >
            <Box sx={style}>
                <Stack direction="row" ml={16} spacing={2} width={"70%"} mt={2} justifyContent="space-between"
                       alignItems="center">
                    <Stack spacing={2}>
                        <Typography variant="subtitle1">
                            Название:
                        </Typography>
                        <TextField
                            id="datetime-local"
                            label="Дата и время"
                            type="datetime-local"
                            defaultValue={dateTime}
                            onChange={e => {
                                setDateTime(e.target.value);
                                check(+new Date(e.target.value))
                            }}
                            InputLabelProps={{
                                shrink: true,
                            }}
                        />
                    </Stack>
                    <Stack spacing={2}>
                        <FormControl fullWidth>
                            <InputLabel id="demo-simple-select-label"
                                        style={{lineHeight: '0.7em', height: '20px'}}>Фотограф</InputLabel>
                            <Select
                                labelId="demo-simple-select-label"
                                id="demo-simple-select"
                                label="Фотограф"
                                value={selectedItem}
                                sx={{height: '40px'}} disabled={!isEnabled}
                                onChange={handleSelect}
                            >
                                {items.map((hall, index) => (
                                    <MenuItem key={index} value={hall.id}>{hall.title}</MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                        <Stack direction="row" width={"80%"} justifyContent="space-between" alignItems="center">
                            <div style={{width: "100px"}}></div>
                            <Stack direction="row" spacing={2}>
                                <Typography variant="subtitle1" style={{whiteSpace: "nowrap"}}>
                                    Стоимость: {getPrice()} рублей
                                </Typography>
                                <Button variant="contained" color="secondary" size="medium" disableElevation
                                        sx={{borderRadius: '10px'}}
                                        onClick={() => {
                                            buy()
                                        }} disabled={!fullEnabled}>
                                    Купить
                                </Button>
                            </Stack>
                        </Stack>
                    </Stack>
                </Stack>
            </Box>
        </Modal>
    )
}