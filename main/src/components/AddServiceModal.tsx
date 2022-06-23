import React from 'react';
import {ServiceModel} from "../models/Models";
import {
    Box, Button,
    Checkbox,
    FormControl,
    FormControlLabel,
    FormGroup,
    InputLabel, MenuItem, Modal, Select,
    Stack,
    TextField,
    Typography
} from "@mui/material";

interface ServiceModalProps {
    open: boolean,
    handlerClose: () => void,
    service: ServiceModel | null
}

export default function AddServiceModal(props: ServiceModalProps){
    const [dateTime, setDateTime] = React.useState<string>();

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
                            }}
                            InputLabelProps={{
                                shrink: true,
                            }}
                        />
                        <TextField label="Период" color='primary' size='small' sx={{width: '100%'}}/>
                        <FormGroup>
                            <FormControlLabel control={<Checkbox defaultChecked/>} label="На все время?"/>
                        </FormGroup>
                    </Stack>
                    <Stack spacing={2}>
                        <FormControl fullWidth>
                            <InputLabel id="demo-simple-select-label"
                                        style={{lineHeight: '0.7em', height: '20px'}}>Услуга</InputLabel>
                            <Select
                                labelId="demo-simple-select-label"
                                id="demo-simple-select"
                                label="Age"
                                sx={{height: '40px'}}
                            >
                                <MenuItem value={1}>Фотографирование</MenuItem>
                                <MenuItem value={2}>Аренда</MenuItem>
                                <MenuItem value={3}>Печать фото</MenuItem>
                            </Select>
                        </FormControl>
                        <TextField label="Количество" color='primary' size='small' sx={{width: '100%'}}/>
                        <Stack direction="row" width={"80%"} justifyContent="space-between" alignItems="center">
                            <div style={{width: "100px"}}></div>
                            <Stack direction="row" spacing={2}>
                                <Typography variant="subtitle1" style={{whiteSpace: "nowrap"}}>
                                    Стоимость: 50 рублей
                                </Typography>
                                <Button variant="contained" color="secondary" size="medium" disableElevation
                                        sx={{borderRadius: '10px'}} onChange={() => {}}>
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