import {createAction} from "@reduxjs/toolkit";

export const OpenModal = createAction<boolean>("OPEN_MODAL");
export const CloseModal = createAction<boolean>("CLOSE_MODAL");