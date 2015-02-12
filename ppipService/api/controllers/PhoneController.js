/**
 * PhoneController
 *
 * @description :: Server-side logic for managing phones
 * @help        :: See http://links.sailsjs.org/docs/controllers
 */

module.exports = {

  register: function (req, res) {
    var deviceName = req.param('deviceName');
    var token = req.param('token');

    if (!deviceName || !token) {
      return res.missingFields(['deviceName', 'token']);
    }

    Phone.update({
      token: token
    }, {
      status: 'online',
      deviceName: deviceName
    })
    .exec(function (err, phones) {
      if (err) {
        return res.serverError(err);
      }

      res.status(200).json(phones);
    });
  },

  refreshToken: function (req, res) {
    var id = req.param('id');

    if (!id) {
      return res.missingFields(['id']);
    }

    Phone.update({
      id: id
    }, {
      token: padNumber(Math.floor(Math.random()*9999), 4),
      status: 'offline',
      deviceName: null
    })
    .exec(function (err, phones) {
      if (err) {
        return res.serverError(err);
      }

      res.status(200).json(phones);
    });
  }

};

function padNumber(number, width, z) {
  z = z || '0';
  number = number + '';
  return number.length >= width ? number : new Array(width - number.length + 1).join(z) + number;
}