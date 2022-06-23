import React, { useState } from 'react';
import {Button, Stack, TextField, Box, Card, FormControlLabel, FormGroup, Checkbox, Typography, Dialog, DialogActions, DialogContent, DialogContentText, FormControl, InputLabel, MenuItem } from "@mui/material";
import IconButton from '@mui/material/IconButton';
import DeleteOutlineOutlinedIcon from '@mui/icons-material/DeleteOutlineOutlined';
import { TypeFlags } from 'typescript';

export default function ShoppingCart() {

    const [dateTime, setDateTime] = React.useState<string>();

    return(
        <div className='section' style={{backgroundColor: '#F0EDE8', borderRadius: '20px', padding: '22px',  width: '100%', height: '100%'}}>
            <Stack direction="row" spacing={8} alignItems='center' justifyContent="center">
                <Typography variant="h6" color="primary" align='center'>Корзина</Typography>
                <Stack direction="row" spacing={2}>
                    <Typography variant="h6" color="primary" align='center'>Выбрано: 2</Typography>
                    <Button variant="contained" color="secondary" size="medium" disableElevation sx={{ borderRadius: '10px'}}>
                        Оплатить
                    </Button>
                </Stack>
            </Stack>

            <Stack spacing={2} sx={{width: "65%", ml: "18%"}} alignItems="center" mt={2}>
                <Card sx={{display: 'flex', border: "3px solid #776D61", borderRadius: "10px", mt: "10px", width: "80%"}}>
                    <Stack direction="row" spacing={8} mt={2} ml={2} mb={2} sx={{width: "100%"}}>
                        <Stack spacing={2} sx={{width: "45%"}}>
                            <TextField label="Название" color='primary' size='small'/>
                            <TextField label="Адрес" color='primary' size='small'/>
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
                            <TextField label="Продолжительность" color='primary' size='small'/>
                        </Stack>
                        <Stack spacing={2}>
                            <TextField label="Цена" color='primary' size='small'/>
                            <Typography>Стилист</Typography>
                            <FormGroup>
                                <FormControlLabel control={<Checkbox defaultChecked />} label="На все время?" />
                            </FormGroup>
                            <TextField label="Выбранный предмет" color='primary' size='small'/>
                        </Stack>
                        <Stack spacing={1} direction={'row'} height={10}>
                            <Checkbox defaultChecked size="medium" />
                            <IconButton aria-label="delete" >
                                <DeleteOutlineOutlinedIcon />
                            </IconButton>
                        </Stack>
                    </Stack>
                </Card>
            </Stack>
        </div>
    )
}
