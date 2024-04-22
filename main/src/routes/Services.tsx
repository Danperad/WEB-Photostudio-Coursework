import {useState, useEffect, ChangeEvent, useRef} from 'react';
import {
    Stack,
    Typography,
    Box,
    TextField,
    InputLabel,
    MenuItem,
    FormControl,
    Select,
    SelectChangeEvent
} from '@mui/material';
import {useDispatch, useSelector} from "react-redux";
import {AppDispatch, RootState} from "../redux/store";
import ServicesService from "../services/ServicesService";
import ServiceModal from "../components/ServiceModal";
import {Service} from "../models/Models";
import InfiniteScroll from "react-infinite-scroll-component";
import {serviceActions} from '../redux/slices/serviceSlice';
import {ServiceCart} from "../components/ServiceCart.tsx";

interface State {
    search: string,
    sort: string,
    type: string
}

export default function Services() {
    const key = useRef(false)
    const [filterState, setFilter] = useState<State>({search: "", sort: '1', type: '0'})
    const [selectedService, setSelected] = useState<Service | null>(null);
    const rootState = useSelector((state: RootState) => state.services);
    const dispatch = useDispatch<AppDispatch>();

    useEffect(() => {
        if (key.current)
            return;
        key.current = true;
        ServicesService.getServices("","1","", 0).then((res) => {
            dispatch(res);
        })
    }, [])

    const [openInfoModal, setOpenInfoModal] = useState(false);

    const handleChange = (prop: keyof State) => (event: ChangeEvent<HTMLInputElement>) => {
        setFilter({...filterState, [prop]: event.target.value.trim()});
        filter(prop, event.target.value.trim())
    };

    const handleChangeSelect = (prop: keyof State) => (event: SelectChangeEvent) => {
        setFilter({...filterState, [prop]: event.target.value});
        filter(prop, event.target.value)
    };

    const filter = (prop: keyof State, value: string) => {
        dispatch(serviceActions.ClearService("hehe"));
        const tmp: State = {...filterState, [prop]: value}
        ServicesService.getServices(tmp.search, tmp.sort, tmp.type, 0).then((res) => {
            dispatch(res);
        })
    }

    const loadMore = () => {
        ServicesService.getServices(filterState.search, filterState.sort, filterState.type, rootState.services.length).then((res) => {
            dispatch(res);
        })
    }
    const handleInfoModalOpen = (service: Service) => {
        setSelected(service);
        setOpenInfoModal(true);
    };

    const closeInfoModal = () => {
        setSelected(null);
        setOpenInfoModal(false);
    };


    return (
        <div style={{width: "100%"}}>
            <Stack direction="row" justifyContent="flex-start" alignItems="center" spacing={2} width={"50%"} mt={2}
                   ml={2}>
                <TextField label="Поиск" value={filterState.search} onChange={handleChange("search")} color='primary'
                           size='small' sx={{width: '100%'}}/>
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
            <Box sx={{width: "95%", margin: '0 auto', marginTop: '40px'}}>
                <Stack>
                    <InfiniteScroll next={loadMore} hasMore={rootState.hasMore} loader={<Typography>Загрузка...</Typography>} dataLength={rootState.services.length}>
                        {rootState.services.map((service) => (
                            <ServiceCart service={service} handleInfoModalOpen={() => {handleInfoModalOpen(service)}} />
                        ))}
                    </InfiniteScroll>
                </Stack>
            </Box>
            <ServiceModal open={openInfoModal} handlerClose={closeInfoModal} service={selectedService}/>
        </div>
    );
}