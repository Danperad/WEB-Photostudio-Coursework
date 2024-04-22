import {useState} from 'react';
import { styled } from '@mui/material/styles';
import {FormGroup, FormControlLabel, Checkbox, Stack, Button, Typography, Box, TextField, InputLabel, MenuItem, FormControl, Select, Grid, Paper, Card, Modal, CardContent,CardMedia} from '@mui/material';

const Item = styled(Paper)(({ theme }) => ({
    backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
    ...theme.typography.body2,
    padding: theme.spacing(1),
    textAlign: 'center',
    color: theme.palette.text.secondary,
}));


export default function Halls() {

    const [openInfoModal, setOpenInfoModal] = useState(false);
    const [openPayModal, setOpenPayModal] = useState(false);

    const handleInfoModalOpen = () => {
        setOpenInfoModal(true);
    };
    const handlePayModalOpen = () => {
        setOpenPayModal(true);
    };
    const closeInfoModal = () => {
        setOpenInfoModal(false);
    };
    const closePayModal = () => {
        setOpenPayModal(false);
    };

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

    const [dateTime, setDateTime] = useState<string>();

    return (
        <div style={{width: "100%"}}>
            <Stack direction="row" justifyContent="flex-start" alignItems="center" spacing={2} width={"50%"} mt={1} ml={2}>
                <TextField label="Поиск" color='primary' size='small' sx={{width: '100%'}}></TextField>
                <FormControl fullWidth>
                    <InputLabel id="demo-simple-select-label" style={{lineHeight: '0.4em', height: '20px', paddingTop: '3px'}}>Сортировать по</InputLabel>
                    <Select
                        labelId="demo-simple-select-label"
                        id="demo-simple-select"
                        label="Age"
                        sx={{height: '40px'}}
                    >
                        <MenuItem value={1}>Популярности</MenuItem>
                        <MenuItem value={2}>Рейтингу</MenuItem>
                        <MenuItem value={3}>Алфавиту</MenuItem>
                    </Select>
                </FormControl>
                <FormControl fullWidth>
                    <InputLabel id="demo-simple-select-label" style={{lineHeight: '0.7em', height: '20px'}}>Услуга</InputLabel>
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
            </Stack>
            <Box sx={{width: "90%", margin: '0 auto', marginTop: '40px'}}>
                <Grid container spacing={{ xs: 2, md: 3 }} columns={{ xs: 4, sm: 8, md: 12 }} justifyContent="center" alignItems="center" >
                    {Array.from(Array(6)).map((_, index) => (
                        <Grid item xs={2} sm={4} md={4} key={index}>
                            <Item sx={{padding: 0}}>
                                <Card>
                                    <CardMedia
                                        component="img"
                                        height="140"
                                        image="../../../public/image/background.png"
                                        alt="photo"
                                    />
                                    <CardContent>
                                        <Typography gutterBottom variant="h5" component="div">
                                            Название
                                        </Typography>
                                        <Typography variant="body2" color="text.secondary">
                                            Lizards are a widespread group of squamate reptiles, with over 6,000
                                            species, ranging across all continents except Antarctica
                                        </Typography>
                                    </CardContent>
                                    <Stack direction="row" justifyContent="space-between" alignItems="center" mr={2} ml={2} pb={1}>
                                        <Typography variant="subtitle1">
                                            Стоимость: 50 рублей
                                        </Typography>
                                        <Button size="medium" variant="contained" color="secondary" onClick={handleInfoModalOpen}>Подробнее</Button>
                                    </Stack>
                                </Card>
                            </Item>
                        </Grid>
                    ))}
                </Grid>
            </Box>
            <Modal
                open={openInfoModal}
                onClose={closeInfoModal}
                aria-labelledby="modal-modal-title"
                aria-describedby="modal-modal-description"
            >
                <Box sx={style} >
                    <Stack spacing={2} width={"100%"} mt={2} alignItems="center" justifyContent={"center"}>
                        <Stack alignItems="center" justifyContent={"center"} style={{height: "400px", width: "600px", borderRadius:"10px" }}>
                            <img
                                src="../../../public/image/background.png"
                                alt="avatar"
                                width="100%"
                                height="100%"
                            />
                        </Stack>
                        <Box sx={{width: "80%"}}>
                            <Typography variant="subtitle1">
                                Название:
                            </Typography>
                            <Typography variant="subtitle1">
                                Описание:
                            </Typography>
                        </Box>
                        <Stack direction="row" width={"80%"} justifyContent="space-between" alignItems="center">
                            <div style={{width: "100px"}}></div>
                            <Stack direction="row" spacing={2}>
                                <Typography variant="subtitle1">
                                    Стоимость: 50 рублей
                                </Typography>
                                <Button variant="contained" color="secondary" size="medium" disableElevation sx={{ borderRadius: '10px'}} onChange={handlePayModalOpen}>
                                    Купить
                                </Button>
                            </Stack>
                        </Stack>
                    </Stack>
                </Box>
            </Modal>
            <Modal
                open={openPayModal}
                onClose={closePayModal}
                aria-labelledby="modal-modal-title"
                aria-describedby="modal-modal-description"
            >
                <Box sx={style} >
                    <Stack direction="row" ml={16} spacing={2} width={"70%"} mt={2} justifyContent="space-between" alignItems="center">
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
                                <FormControlLabel control={<Checkbox defaultChecked />} label="На все время?" />
                            </FormGroup>
                        </Stack>
                        <Stack spacing={2}>
                            <FormControl fullWidth>
                                <InputLabel id="demo-simple-select-label" style={{lineHeight: '0.7em', height: '20px'}}>Услуга</InputLabel>
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
                                    <Button variant="contained" color="secondary" size="medium" disableElevation sx={{ borderRadius: '10px'}} onChange={handlePayModalOpen}>
                                        Купить
                                    </Button>
                                </Stack>
                            </Stack>
                        </Stack>
                    </Stack>
                </Box>
            </Modal>
        </div>
    );
}