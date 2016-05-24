using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using BLL.Abstract;
using Lab1_des.Models;

namespace Lab1_des.ApiControllers
{
    public class KeyController : BaseApiController
    {
        private readonly IKeyService _keyService;

        public KeyController(IKeyService keyService)
        {
            _keyService = keyService;
        }

        [HttpGet]
        [Route("api/keys/simple")]
        public IHttpActionResult GenerateKey()
        {
            var key = new SimpleKeyViewModel
            {
                Key = _keyService.GenerateSimpleKey()
            };

            return Ok(key);
        }

        [HttpGet]
        [Route("api/keys/rsa")]
        public IHttpActionResult GenerateRsaKeys()
        {
            var rsa = _keyService.GenerateRsaKey();

            return Ok(Mapper.Map<RSAKeysViewModel>(rsa));
        }

		[HttpGet]
		[Route("api/keys/hash")]
		public IHttpActionResult GenerateHash(string text)
		{
			return Ok(Hash(text));
		}

		private uint Hash(string key)
		{
			uint hash = 0;
			int i =0;
			for (; i < key.Length; ++i)
			{
				hash += key[i];
				hash += (hash << 10);
				hash ^= (hash >> 6);
			}
			hash += (hash << 3);
			hash ^= (hash >> 11);
			hash += (hash << 15);

			return hash;
		}
	}
}
