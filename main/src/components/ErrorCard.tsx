import React from 'react';
import { useSnackbar } from 'notistack';
import {useDispatch, useSelector} from "react-redux";
import {RootState} from "../redux/store";
import {snackbarActions} from "../redux/slices/snackbarSlice";

function ErrorCard(){
    const { enqueueSnackbar } = useSnackbar();
    const dispatch = useDispatch();
    const state = useSelector((state: RootState) => state.messages.state);

    const close = (key: number) => {
        dispatch(snackbarActions.RemoveAction(key));
    }

    React.useEffect(() => {
        state.forEach((s) => {
            enqueueSnackbar(s.error, {
                key: s.key,
                onExited: () => {close(s.key)},

            })
        })
    },[state, enqueueSnackbar, close])

    return <></>;
}

export default ErrorCard;