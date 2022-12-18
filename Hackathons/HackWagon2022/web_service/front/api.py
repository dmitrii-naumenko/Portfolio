import dash
from dash import dcc, html
from dash.dependencies import Input, Output, State
import os

import requests

import pandas as pd
import numpy as np

URL = f'http://{os.environ["BACK_URL"] if "BACK_URL" in os.environ else "192.168.0.19:5000"}'

parameters = \
    {
        'st_code_snd':['', 'str'],
        'st_code_rsv':['', 'str'],
        'date_depart_year':[0, 'int64'],
        'date_depart_month':[0, 'int64'],
        'date_depart_week':[0, 'int64'],
        'date_depart_day':[0, 'int64'],
        'date_depart_hour':[0, 'int64'],
        'fr_id':[0, 'float64'],
        'route_type':[0, 'float64'],
        'is_load':[0, 'int64'],
        'rod':[0, 'int64'],
        'common_ch':[0, 'float64'],
        'vidsobst':[0, 'float64'],
        'distance':[0, 'float64'],
        'snd_org_id':[0, 'int64'],
        'rsv_org_id':[0, 'int64'],
        'snd_roadid':[0, 'int32'],
        'rsv_roadid':[0, 'int32'],
        'snd_dp_id':[0, 'int64'], 
        'rsv_dp_id':[0, 'int64']}
data = {}

app = dash.Dash(__name__)
server = app.server

children = [
        html.H1(children="Сервис для прогнозирования длительности движения вагонов",),
        html.Hr()]

for n in parameters.keys():
    children.append(html.P(children=f'Введите значение {n}:', style={'margin-left': '25px'}));
    
    children.append(
        html.Div(dcc.Input(id='input-'+n, placeholder='<Введите значение>', type='text', \
                       style={'width': '25%', 'border-style': 'solid', 'border-width': '1px', \
                              'border-color': 'gray', 'margin-left': '25px'})));
        
children.append(html.Div(children=[
    html.Br(),
    html.Br(),
    html.Button('Обработать', id='submit-val', 
                style={'background': '#0066A2', 'color': 'white', 'border-style': 'outset', \
                       'border-color': '#0066A2', 'height': '50px', 'width': '100px', \
                       'font': 'bold15px arial,sans-serif', 'text-shadow': 'none'}),
    html.Br(),
    html.Br(),
    html.Br(),
    html.P(children='Оценка длительности:'),
    html.Div(id='container-button-basic', children='Нажмите "Обработать" для получения результата')
]));

app.layout = html.Div(children=children)


@app.callback(
    Output('container-button-basic', 'children'),
    Input('submit-val', 'n_clicks'),
    [State('input-st_code_snd', 'value'), \
     State('input-st_code_rsv', 'value'), \
     State('input-date_depart_year', 'value'), \
     State('input-date_depart_month', 'value'), \
     State('input-date_depart_week', 'value'), \
     State('input-date_depart_day', 'value'), \
     State('input-date_depart_hour', 'value'), \
     State('input-fr_id', 'value'), \
     State('input-is_load', 'value'), \
     State('input-rod', 'value'), \
     State('input-common_ch', 'value'), \
     State('input-vidsobst', 'value'), \
     State('input-distance', 'value'), \
     State('input-snd_org_id', 'value'), \
     State('input-rsv_org_id', 'value'), \
     State('input-snd_roadid', 'value'), \
     State('input-rsv_roadid', 'value'), \
     State('input-snd_dp_id', 'value'), \
     State('input-rsv_dp_id', 'value') \
    ]
)
def predict(n_clicks, st_code_snd, st_code_rsv, date_depart_year, date_depart_month,
              date_depart_week, date_depart_day, date_depart_hour, fr_id, is_load, 
              rod, common_ch, vidsobst, distance, snd_org_id, rsv_org_id, snd_roadid, 
              rsv_roadid, snd_dp_id, rsv_dp_id):

    params = {}
    print('pararams')
    if st_code_snd is not None:
        params['st_code_snd'] = st_code_snd
        
    if st_code_rsv is not None:
        params['st_code_rsv'] = st_code_rsv

    if date_depart_year is not None:
        params['date_depart_year'] = date_depart_year

    if date_depart_month is not None:
        params['date_depart_month'] = date_depart_month

    if date_depart_week is not None:
        params['date_depart_week'] = date_depart_week

    if date_depart_day is not None:
        params['date_depart_day'] = date_depart_day

    if date_depart_hour is not None: 
        params['date_depart_hour'] = date_depart_hour

    if fr_id is not None:
        params['fr_id'] = fr_id

    if is_load is not None: 
        params['is_load'] = is_load

    if rod is not None:
        params['rod'] = rod

    if common_ch is not None: 
        params['common_ch'] = common_ch

    if vidsobst is not None:
        params['vidsobst'] = vidsobst

    if distance is not None:
        params['distance'] = distance

    if snd_org_id is not None: 
        params['snd_org_id'] = snd_org_id

    if rsv_org_id is not None:
        params['rsv_org_id'] = rsv_org_id

    if snd_roadid is not None:
        params['snd_roadid'] = snd_roadid

    if rsv_roadid is not None:
        params['rsv_roadid'] = rsv_roadid

    if snd_dp_id is not None:
        params['snd_dp_id'] = snd_dp_id

    if rsv_dp_id is not None:
        params['rsv_dp_id'] = rsv_dp_id

    responce = requests.get(f'{URL}/predict_delivery_time', params=params)
    return f'Результат = {responce.text}'


if __name__ == '__main__':
    # from werkzeug.serving import run_simple
    print('start from main front')
    # run_simple('127.0.0.1', 8050, server)
    app.run(host='0.0.0.0')
