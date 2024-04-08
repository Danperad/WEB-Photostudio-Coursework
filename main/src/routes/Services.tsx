import {ChangeEvent, useEffect, useRef, useState} from 'react';
import {
  Box,
  Button,
  Card,
  CardContent,
  CardMedia,
  FormControl,
  InputLabel,
  MenuItem, Portal,
  Select,
  SelectChangeEvent,
  Stack,
  TextField,
  Typography
} from '@mui/material';
import {useDispatch, useSelector} from "react-redux";
import {AppDispatch, RootState} from "../redux/store";
import ServicesService from "../services/ServicesService";
import ServiceModal from "../components/ServiceModal";
import {Service} from "../models/Models";
import InfiniteScroll from "react-infinite-scroll-component";
import {serviceActions} from '../redux/slices/serviceSlice';
import {StartTutorial} from "../redux/actions/tutorialActions.ts";

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
  const tutorialState = useSelector((state: RootState) => state.tutorial);
  const dispatch = useDispatch<AppDispatch>();
  const [openInfoModal, setOpenInfoModal] = useState(false);

  useEffect(() => {
    if (key.current)
      return;
    key.current = true;
    ServicesService.getServices("", "", "", 0).then((res) => {
      dispatch(res);
    })
  }, [dispatch])

  useEffect(() => {
    if (rootState.services.length === 0 || !tutorialState.isInit || tutorialState.isExit || tutorialState.started)
      return
    dispatch(StartTutorial())
  }, [rootState, dispatch]);

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
    <div style={{width: "100%"}} id={"services-page"}>
      <Stack direction="row" justifyContent="flex-start" alignItems="center" spacing={2} width={"50%"} mt={1}
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
      <Box sx={{width: "90%", margin: '0 auto', marginTop: '40px'}}>
        <Stack>
          <InfiniteScroll next={loadMore} hasMore={rootState.hasMore} loader={<Typography>Загрузка...</Typography>}
                          dataLength={rootState.services.length}>
            {rootState.services.map((service, i) => (
              <Card id={`service-${i}`} key={service.id} sx={{mt: 2}}>
                <Stack direction={'row'}>
                  <CardMedia
                    component={"img"}
                    height={"140"}
                    sx={{width: '30%'}}
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
                  <Stack direction="row" justifyContent="space-between" alignItems={'flex-end'} mr={2}
                         ml={2}
                         pb={1} spacing={1}>
                    <Typography variant="subtitle1" style={{whiteSpace: "nowrap"}}>
                      Стоимость: {service.cost} рублей
                    </Typography>
                    <Button id={`service-buy-${i}`} size="medium" variant="contained" color="secondary"
                            onClick={() => {
                              handleInfoModalOpen(service)
                            }}>Подробнее</Button>
                  </Stack>
                </Stack>
              </Card>
            ))}
          </InfiniteScroll>
        </Stack>
      </Box>
      <ServiceModal open={openInfoModal} handlerClose={closeInfoModal} service={selectedService}/>
      {(!tutorialState.started || tutorialState.isExit) && (
        <>
          <Box id={"service-modal-portal-box"}/>
          <Box id={"add-service-modal-portal-box"}/>
        </>
      )}
    </div>
  );
}
