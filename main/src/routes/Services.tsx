import React, {useState} from 'react';
import {styled} from '@mui/material/styles';
import {
    Stack,
    Button,
    Typography,
    Box,
    TextField,
    InputLabel,
    MenuItem,
    FormControl,
    Select,
    Grid,
    Paper,
    Card,
    CardContent,
    CardMedia,
    SelectChangeEvent
} from '@mui/material';
import {useDispatch, useSelector} from "react-redux";
import {AppDispatch, RootState} from "../redux/store";
import ServicesService from "../services/ServicesService";
import ServiceModal from "../components/ServiceModal";
import {ServiceModel} from "../models/Models";

const Item = styled(Paper)(({theme}) => ({
    backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
    ...theme.typography.body2,
    padding: theme.spacing(1),
    textAlign: 'center',
    color: theme.palette.text.secondary,
}));

interface State {
    search: string,
    sort: string,
    type: string
}

export default function Services() {
    const [key, setKey] = useState<boolean>(false);
    const [filterState, setFilter] = useState<State>({search: "", sort: '1', type: '0'})
    const [selectedService, setSelected] = useState<ServiceModel | null>(null);
    const rootState = useSelector((state: RootState) => state.services);
    const dispatch = useDispatch<AppDispatch>();

    React.useEffect(() => {
        if (key) return;
        ServicesService.getServices("","1","0").then((res) => {
            dispatch(res);
        })
        setKey(true);
    }, [key, dispatch])

    const [openInfoModal, setOpenInfoModal] = useState(false);

    const handleChange = (prop: keyof State) => (event: React.ChangeEvent<HTMLInputElement>) => {
        setFilter({...filterState, [prop]: event.target.value.trim()});
        filter(prop, event.target.value.trim())
    };

    const handleChangeSelect = (prop: keyof State) => (event: SelectChangeEvent) => {
        setFilter({...filterState, [prop]: event.target.value});
        filter(prop, event.target.value)
    };

    const filter = (prop: keyof State, value: string) => {
        const tmp : State = {...filterState, [prop]: value}
        ServicesService.getServices(tmp.search,tmp.sort,tmp.type).then((res) => {
            dispatch(res);
        })
    }

    const handleInfoModalOpen = (service: ServiceModel) => {
        setSelected(service);
        setOpenInfoModal(true);
    };

    const closeInfoModal = () => {
        setSelected(null);
        setOpenInfoModal(false);
    };


    return (
        <div style={{width: "100%"}}>
            <Stack direction="row" justifyContent="flex-start" alignItems="center" spacing={2} width={"50%"} mt={1}
                   ml={2}>
                <TextField label="Поиск" value={filterState.search} onChange={handleChange("search")} color='primary' size='small' sx={{width: '100%'}} />
                <FormControl fullWidth>
                    <InputLabel id="demo-simple-select-label"
                                style={{lineHeight: '0.9em', height: '20px'}}>Тип услуги</InputLabel>
                    <Select
                        labelId="demo-simple-select-label"
                        id="demo-simple-select"
                        label="Тип услуги"
                        sx={{height: '40px'}}
                        value={filterState.type}
                        onChange={handleChangeSelect('type')}
                    >
                        <MenuItem value={0}>Любой</MenuItem>
                        <MenuItem value={3}>Фото и видео</MenuItem>
                        <MenuItem value={2}>Аренда помещения</MenuItem>
                        <MenuItem value={4}>Аренда реквизита</MenuItem>
                        <MenuItem value={5}>Стилист</MenuItem>
                        <MenuItem value={1}>Дополнительные услуги</MenuItem>
                    </Select>
                </FormControl>
                <FormControl sx={{width: '70%'}}>
                    <InputLabel id="demo-simple-select-label"
                                style={{lineHeight: '0.4em', height: '20px', paddingTop: '3px'}}>Сортировать
                        по</InputLabel>
                    <Select
                        labelId="demo-simple-select-label"
                        id="demo-simple-select"
                        label="Сортировать по"
                        sx={{height: '40px'}}
                        value={filterState.sort}
                        onChange={handleChangeSelect('sort')}
                    >
                        <MenuItem value={1}>Алфавиту</MenuItem>
                        <MenuItem value={2}>Цене</MenuItem>
                        <MenuItem value={3}>Рейтингу</MenuItem>
                    </Select>
                </FormControl>
            </Stack>
            <Box sx={{width: "90%", margin: '0 auto', marginTop: '40px'}}>
                <Grid container spacing={{xs: 2, md: 3}} columns={{xs: 4, sm: 8, md: 12}} justifyContent="center"
                      alignItems="center">
                    {rootState.map((service, index) => (
                        <Grid item xs={2} sm={4} md={4} key={index}>
                            <Item sx={{padding: 0}}>
                                <Card>
                                    <CardMedia
                                        component="img"
                                        height="140"
                                        image={service.photos[0]}
                                        alt="photo"
                                    />
                                    <CardContent>
                                        <Typography gutterBottom variant="h5" component="div">
                                            {service.title}
                                        </Typography>
                                        <Typography variant="body2" color="text.secondary">
                                            {service.description}
                                        </Typography>
                                    </CardContent>
                                    <Stack direction="row" justifyContent="space-between" alignItems="center" mr={2}
                                           ml={2} pb={1}>
                                        <Typography variant="subtitle1">
                                            Стоимость: {service.cost} рублей
                                        </Typography>
                                        <Button size="medium" variant="contained" color="secondary" onClick={() => {
                                            handleInfoModalOpen(service)
                                        }}>Подробнее</Button>
                                    </Stack>
                                </Card>
                            </Item>
                        </Grid>
                    ))}
                </Grid>
            </Box>
            <ServiceModal open={openInfoModal} handlerClose={closeInfoModal} service={selectedService}/>
        </div>
    );
}