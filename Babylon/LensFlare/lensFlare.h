#ifndef BABYLON_LENSFLARE_H
#define BABYLON_LENSFLARE_H

#include <memory>
#include <vector>
#include <map>

#include "iengine.h"
#include "vector3.h"
#include "color3.h"
#include "texture.h"

using namespace std;

namespace Babylon {

	class LensFlareSystem;
	typedef shared_ptr<LensFlareSystem> LensFlareSystemPtr;

	class LensFlare : public enable_shared_from_this<LensFlare> {

	public:

		typedef shared_ptr<LensFlare> Ptr;
		typedef vector<Ptr> Array;

		Color3::Ptr color;
		Vector3::Ptr position;
		float size;
		Texture::Ptr texture;
		LensFlareSystemPtr _system;

	public: 
		LensFlare(float size, Vector3::Ptr position, Color3::Ptr color, string imgUrl, LensFlareSystemPtr system);

		// Methods
		virtual void dispose();
	};

};

#endif // BABYLON_LENSFLARE_H