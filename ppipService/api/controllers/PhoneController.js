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
  }

};

