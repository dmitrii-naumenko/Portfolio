import flask
from flask import request, jsonify
import jsonpickle

import pickle
import pandas as pd
import numpy as np
import sklearn

model = pickle.load(open('model-bk-47.pickle', 'rb'))

def predict_delivery_time(df):
    ans = model.predict(df)
    print('ansswer', ans)
    return ans

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
        'rsv_dp_id':[0, 'int64'],
        'route_category':['', 'str'],
        'dayofweek':['', 'str'],
        }
data = {}

app = flask.Flask(__name__)
app.config["DEBUG"] = True

@app.route('/predict_delivery_time', methods=['GET'])
def get_predict_delivery_time():
    print('get_predict_delivey_time')
    print("parameters", parameters)

    for n in parameters.keys():
        if n in request.args:
            data[n] = [str(request.args[n])]
        else:
            data[n] = [parameters[n][0]]
    
    df = pd.DataFrame(data)
    for n in parameters.keys():
        df[n] = df[n].astype(parameters[n][1])
    
    delivery_time = predict_delivery_time(df)
    return str(delivery_time)


if __name__ == '__main__':
    print('start from main')
    # from werkzeug.serving import run_simple
    # run_simple('localhost', 5000, app)
    app.run(host='0.0.0.0', port=5000)
