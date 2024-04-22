import {useState} from 'react';
import {NewServicePackage, ServicePackage} from "../models/Models";
import {Box, Button, Modal, Stack, TextField, Typography} from "@mui/material";
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
    const [isEnabled, setEnabled] = useState<boolean>(false);

    const check = (date: number) => {
        if (date < +Date.now()) {
            setEnabled(false);
            return;
        }
        setEnabled(true);
    }
    const style = {
        position: 'absolute' as const,
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

    const buy = () => {
        const newService: NewServicePackage = {
            service: props.service!,
            startTime: +new Date(dateTime),
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
                        <Stack direction="row" width={"80%"} justifyContent="space-between" alignItems="center">
                            <div style={{width: "100px"}}></div>
                            <Stack direction="row" spacing={2}>
                                <Typography variant="subtitle1" style={{whiteSpace: "nowrap"}}>
                                    Стоимость: {props.service!.cost} рублей
                                </Typography>
                                <Button variant="contained" color="secondary" size="medium" disableElevation
                                        sx={{borderRadius: '10px'}}
                                        onClick={() => {
                                            buy()
                                        }} disabled={!isEnabled}>
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