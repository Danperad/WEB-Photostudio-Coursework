import {useState} from "react";
import dayjs, {Dayjs} from "dayjs";
import {Button, Stack} from "@mui/material";
import {DatePicker} from "@mui/x-date-pickers";
import {getReport} from "../../services/reportService.ts";

function Reports(){
  const [startDate, setStartDate] = useState<Dayjs>(dayjs());
  const [endDate, setEndDate] = useState<Dayjs>(dayjs());

  const startDateSelectHandler = (date: Dayjs | null) => {
    if (date === null)
      return
    setStartDate(date)
  }

  const endDateSelectHandler = (date: Dayjs | null) => {
    if (date === null)
      return
    setEndDate(date)
  }

  const handleReportDownloadClick = () => {
    getReport(startDate.toDate(), endDate.toDate())
  };

  return (
    <Stack>
      <Stack direction={"row"}>
        <DatePicker label={"Дата"} format={"DD-MM-YYYY"} defaultValue={dayjs()} value={startDate}
                    onChange={startDateSelectHandler} sx={{width: "8vw"}}/>
        <DatePicker label={"Дата"} format={"DD-MM-YYYY"} defaultValue={dayjs()} value={endDate}
                    onChange={endDateSelectHandler} sx={{width: "8vw"}}/>
      </Stack>
      <Button variant={"contained"} onClick={handleReportDownloadClick}>Скачать отчет</Button>
    </Stack>
  )
}

export {Reports}
