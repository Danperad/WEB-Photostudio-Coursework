import {useEffect, useState} from 'react';
import { useSnackbar } from 'notistack';
import {useDispatch, useSelector} from "react-redux";
import {RootState} from "../redux/store";
import {snackbarActions} from "../redux/slices/snackbarSlice";

function ErrorCard(){
    const { enqueueSnackbar } = useSnackbar();
    const [viewd, setViewd] = useState<number[]>([]);
    const dispatch = useDispatch();
    const state = useSelector((state: RootState) => state.messages.state);

    useEffect(() => {
        state.forEach((s) => {
            if (viewd.indexOf(s.key) === -1){
                enqueueSnackbar(s.error, {
                    key: s.key,
                    onEnter: () => {
                        const tmp = [...viewd];
                        tmp.push(s.key);
                        setViewd(tmp);
                    },
                    onExited: () => {
                        setViewd(viewd.filter(v => v !== s.key))
                        dispatch(snackbarActions.RemoveAction(s.key))
                    },
                })
            }
        })
    },[state])

    return undefined;
}

export default ErrorCard;